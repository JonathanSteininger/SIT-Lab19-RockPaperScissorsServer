using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RockPaperScissorNetworkLibrary;

namespace ServerForm
{
    public class ClientThreadManager : IEnumerable
    {
        private List<ClientWorker> Workers;
        public GroupStopper Stopper = new GroupStopper();

        public ClientThreadManager()
        {
            Workers = new List<ClientWorker>();
        }

        public void Add(ClientWorker Worker)
        {
            Worker.GroupStopper = Stopper;
            Workers.Add(Worker);
        }
        public ClientWorker this[int index] { get { return Workers[index]; } set { Workers[index] = value; } }

        /// <summary>
        /// Searches for the first ClientWorker stored insied this instance that meet the requirmets specified.
        /// returns null if nothing was found.
        /// </summary>
        /// <param name="match">Condition</param>
        /// <returns>Matching Clientworker, or null</returns>
        /// <exception cref="ArgumentNullException">if the condition was null</exception>
        public ClientWorker Find(Predicate<ClientWorker> match)
        {
            if(match == null) throw new ArgumentNullException("match");
            foreach(ClientWorker worker in Workers)
            {
                if (match(worker))
                {
                    return worker;
                }
            }
            return null;
        }
        /// <summary>
        /// Searches for the index of the first ClientWorker stored insied this instance that meet the requirmets specified.
        /// returns -1 if nothing was found.
        /// </summary>
        /// <param name="match">Condition</param>
        /// <returns>Matching ClientWorker index, or -1</returns>
        /// <exception cref="ArgumentNullException">if the condition was null</exception>
        public int FindIndex(Predicate<ClientWorker> match)
        {
            if (match == null) throw new ArgumentNullException("match");
            for (int i = 0; i < Workers.Count; i++)
                if (match(Workers[i])) return i;
            return -1;
        }

        public List<ClientWorker> GetClients()
        {
            return Workers;
        }
        public List<GameData> GetAllGames()
        {
            List<GameData> games = new List<GameData>();
            foreach(ClientWorker worker in Workers)
            {
                if (!games.Contains(worker.GameManager.Game)) 
                    games.Add(worker.GameManager.Game);
            }
            return games;
        }
        /// <summary>
        /// Pauses all workers stored in this instance
        /// </summary>
        public void PauseAll() => Stopper.Pause();

        /// <summary>
        /// Resumes all workers stored in this instance
        /// </summary>
        public void ResumeAll() => Stopper.Resume();

        /// <summary>
        /// Stops all Workers stored in this instance permenantly
        /// </summary>
        public void StopAll() { foreach (ClientWorker Worker in Workers) Worker.Stop(); }

        /// <summary>
        /// Joins all worker's threads, will be slow if run by itself. as each thread needs to be completed before attempting to join the next.
        /// </summary>
        public void JoinAll() { foreach (ClientWorker Worker in Workers) Worker.Join(); }
        /// <summary>
        /// Stops, joins, and disposes all workers from the list. recommended for removing all workers.
        /// </summary>
        public void DisposeAll() {
            StopAll();
            JoinAll();
            foreach (ClientWorker Worker in Workers) Worker.Dispose();
            Workers.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return new ClientWorkerEnumerable(Workers);
        }
    }
    /// <summary>
    /// Used to enumerate ClientWorkers
    /// </summary>
    public class ClientWorkerEnumerable : IEnumerator
    {
        private int index = -1;
        public object Current => _workers[index];

        private ClientWorker[] _workers;
        public ClientWorkerEnumerable(ClientWorker[] workers) => _workers = workers;
        public ClientWorkerEnumerable(List<ClientWorker> workers) => _workers = workers.ToArray();


        public bool MoveNext()
        {
            index++;
            return index < _workers.Length;
        }

        public void Reset()
        {
            index = -1;
        }
    }

}
