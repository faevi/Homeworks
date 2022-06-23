using System;
namespace ConsoleApp
{
    public class UniqueList<T> : List<T>
    {
        public UniqueList() : base() { }

        public UniqueList(IEnumerable<T> collection)
            : base(collection)
        {
            foreach (T element in collection)
            {
                if (collection.Where(value => value!.Equals(element)).Count() > 1)
                {
                    throw new DublicateException<T>($"Object with value {element} already exist", element);
                }
            }
        }

        public new void Add(T value)
        {
            // При добавление существуещего элемента
            if (this.Where(x => x.Equals(value)).Count() != 0)
            {
                throw new DublicateException<T>($"Object with value {value} already exist", value);
            }

            base.Add(value);
        }

        public new bool Remove(T value)
        {
            if (this.Where(x => x.Equals(value)).Count() == 0)
            {
                throw new IncorrectRemoveException<T>($"There are no object with value: {value}", value);
            }

            base.Remove(value);
            return true;
        }

        public void ShowUniqueList()
        {
            if (this.Count() == 0)
            {
                Console.WriteLine("No elements in current UniqueList");
            }

            for (int index = 0; index < this.Count(); index++)
            {
                Console.WriteLine("In index {0} value: {1}", index, this[index]);
            }
        }
    }
}