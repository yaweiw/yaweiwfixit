using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservableCollection
{
    public class DictionaryWatcher : ObservableCollection<KeyValuePair<string, string>>, IDisposable
    {
        private NotifyCollectionChangedEventHandler watcher;
        private bool watching = false;

        public DictionaryWatcher()
        {
            watcher = new NotifyCollectionChangedEventHandler(ReportChange);
            CollectionChanged += watcher;
            Watched = true;
        }

        public bool Watched
        {
            get
            {
                return watching;
            }

            set
            {
                if (watching)
                {
                    lock (this)
                    {
                        CollectionChanged -= watcher;
                        watching = false;
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Watched)
            {
                Watched = false;
            }
        }
        public void Initialize()
        {
            this.Add(new KeyValuePair<string, string>("First", "1"));
            this.Add(new KeyValuePair<string, string>("Second", "2"));
            this.Add(new KeyValuePair<string, string>("Turd", "3"));
            KeyValuePair<string, string> badValue = this[2];
            this.Remove(badValue);
        }

        private void ReportChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Change made: {0}", e.Action);
        }
    }

    public class ObservableCollectionHandler
    {

        public ObservableCollection<string> Collection { get; set; }

        public ObservableCollectionHandler()
        {
            Collection = new ObservableCollection<string>();
            Collection.CollectionChanged += HandleChange;
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var x in e.NewItems)
                {
                    // do something
                }

            }
            //modification will be Replace
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (var y in e.OldItems)
                {
                    //do something
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                //do something
            }
        }
    }
    class program
    {
        static void Main(string[] args)
        {
            //DictionaryWatcher dw = new DictionaryWatcher();
            //dw.Initialize();
            ObservableCollectionHandler oc = new ObservableCollectionHandler();

            oc.Collection.Add("first");
            oc.Collection.Add("second");

            oc.Collection[0] = "1st";
            oc.Collection[1] = "2nd";

            Console.ReadLine();
        }
    }
}
