﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StategyPattern
{    
    // Контекст определяет интерфейс, представляющий интерес для клиентов.
    class Context
    {
        // Контекст хранит ссылку на один из объектов Стратегии. Контекст не
        // знает конкретного класса стратегии. Он должен работать со всеми
        // стратегиями через интерфейс Стратегии.
        private IStrategy _strategy;

        public Context()
        { }

        // Обычно Контекст принимает стратегию через конструктор, а также
        // предоставляет сеттер для её изменения во время выполнения.
        public Context(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Обычно Контекст позволяет заменить объект Стратегии во время
        // выполнения.
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Вместо того, чтобы самостоятельно реализовывать множественные
        // версии алгоритма, Контекст делегирует некоторую работу объекту
        // Стратегии.
        public void GoToStadion()
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");
            var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    // Интерфейс Стратегии объявляет операции, общие для всех поддерживаемых
    // версий некоторого алгоритма.
    //
    // Контекст использует этот интерфейс для вызова алгоритма, определённого
    // Конкретными Стратегиями.
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // Конкретные Стратегии реализуют алгоритм, следуя базовому интерфейсу
    // Стратегии. Этот интерфейс делает их взаимозаменяемыми в Контексте.
    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код выбирает конкретную стратегию и передаёт её в
            // контекст. Клиент должен знать о различиях между стратегиями,
            // чтобы сделать правильный выбор.
            var context = new Context();

            Console.WriteLine("Client: Strategy is set to normal sorting.");
            context.SetStrategy(new ConcreteStrategyA());
            context.GoToStadion();

            Console.WriteLine();

            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            context.SetStrategy(new ConcreteStrategyB());
            context.GoToStadion();
        }
    }
}
