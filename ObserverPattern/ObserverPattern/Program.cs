using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObserverPattern
{
    public interface IObserver
    {
        // Получает обновление от издателя
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        // Присоединяет наблюдателя к издателю.
        void Attach(IObserver observer);

        // Отсоединяет наблюдателя от издателя.
        void Detach(IObserver observer);

        // Уведомляет всех наблюдателей о событии.
        void NotifyNewspappersObserver();
    }

    // Издатель владеет некоторым важным состоянием и оповещает наблюдателей о
    // его изменениях.
    public class Subject : ISubject
    {
        // Для удобства в этой переменной хранится состояние Издателя,
        // необходимое всем подписчикам.
        public int NewspapperState { get; set; } = -0;
        public int JournalState { get; set; } = -0;

        // Список подписчиков. В реальной жизни список подписчиков может
        // храниться в более подробном виде (классифицируется по типу события и
        // т.д.)
        private List<IObserver> _newspappersObserver = new List<IObserver>();
        private List<IObserver> _journalsObserver = new List<IObserver>();

        // Методы управления подпиской.
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._newspappersObserver.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._newspappersObserver.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        // Запуск обновления в каждом подписчике.
        public void NotifyNewspappersObserver()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _newspappersObserver)
            {
                observer.Update(this);
            }
        }

        public void NotifyJournalsObserver()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _journalsObserver)
            {
                observer.Update(this);
            }
        }

        // Обычно логика подписки – только часть того, что делает Издатель.
        // Издатели часто содержат некоторую важную бизнес-логику, которая
        // запускает метод уведомления всякий раз, когда должно произойти что-то
        // важное (или после этого).
        public void JournalLogic()
        {
            Console.WriteLine("\nJournal: I'm doing something important.");
            this.JournalState = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Journal: My state has just changed to: " + this.JournalState);
            this.NotifyJournalsObserver();
        }

        public void NewspapperLogic()
        {
            Console.WriteLine("\nNewspapper: I'm doing something important.");
            this.NewspapperState = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Newspapper: My state has just changed to: " + this.NewspapperState);
            this.NotifyNewspappersObserver();

        }
    }

    // Конкретные Наблюдатели реагируют на обновления, выпущенные Издателем, к
    // которому они прикреплены.
    class FirstClient : IObserver
    {
        public void Update(ISubject subject)
        {
            Console.WriteLine("Inside FirstClient");
            if ((subject as Subject).NewspapperState < 3)
            {
                Console.WriteLine("FirstClient: Reacted to the event.");
            }
        }
    }

    class SecondClient : IObserver
    {
        public void Update(ISubject subject)
        {
            Console.WriteLine("Inside SecondClient");
            if ((subject as Subject).NewspapperState == 0 || (subject as Subject).NewspapperState >= 2)
            {
                Console.WriteLine("SecondClient: Reacted to the event.");
            }
        }
    }

    class ThirdClient : IObserver
    {
        public void Update(ISubject subject)
        {
            Console.WriteLine("Inside ClientThird");
            if ((subject as Subject).JournalState < 3)
            {
                Console.WriteLine("ClientThird: Reacted to the event.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код.
            var subject = new Subject();//магазин
            var observerFirst = new FirstClient();//Наблюдатель первый
            subject.Attach(observerFirst);

            var observerSecond = new SecondClient();//Наблюдатель второй
            subject.Attach(observerSecond);

            var observerThird = new ThirdClient();
            subject.Attach(observerThird);

            subject.NewspapperLogic();
            subject.NewspapperLogic();
            subject.JournalLogic();

            subject.Detach(observerSecond);

            subject.NewspapperLogic();
        }
    }
}
