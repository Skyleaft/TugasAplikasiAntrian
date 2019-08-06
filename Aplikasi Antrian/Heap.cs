using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Antrian
{
    public class Heap<T> where T : IComparable
    {
        //data nya masukin ke list
        private List<T> elements = new List<T>();

        //buat dapetin jumlah data
        public int GetSize()
        {
            return elements.Count;
        }

        public T GetMin()
        {
            return this.elements.Count > 0 ? this.elements[0] : default(T);
        }
        public bool IsEmpty { get { return elements.Count == 0; } }

        public void Tambah(T item)
        {
            elements.Add(item);
            this.HeapifyUp(elements.Count - 1);
        }

        //ambil data
        public T Ambil()
        {
            if (elements.Count > 0)
            {
                T item = elements[0];
                elements[0] = elements[elements.Count - 1];
                elements.RemoveAt(elements.Count - 1);

                this.HeapifyDown(0);
                return item;
            }

            throw new InvalidOperationException("no element in heap");
        }

        //heapifyUp == ShiftUp
        private void HeapifyUp(int index)
        {
            var parent = this.GetParent(index);
            if (parent >= 0 && elements[index].CompareTo(elements[parent]) < 0)
            {
                var temp = elements[index];
                elements[index] = elements[parent];
                elements[parent] = temp;

                this.HeapifyUp(parent);
            }
        }

        //heapifyDown == ShiftDown
        private void HeapifyDown(int index)
        {
            var terkecil = index;

            var left = this.GetLeft(index);
            var right = this.GetRight(index);

            //membandingkan data mana yang lebih kecil dari left dan elemen/data nya lebih kecil
            if (left < this.GetSize() && elements[left].CompareTo(elements[index]) < 0)
            {
                terkecil = left;
            }

            if (right < this.GetSize() && elements[right].CompareTo(elements[terkecil]) < 0)
            {
                terkecil = right;
            }

            if (terkecil != index)
            {
                var temp = elements[index];
                elements[index] = elements[terkecil];
                elements[terkecil] = temp;

                this.HeapifyDown(terkecil);
            }

        }

        private int GetParent(int index)
        {
            if (index <= 0)
            {
                return -1;
            }

            return (index - 1) / 2;
        }

        private int GetLeft(int index)
        {
            return 2 * index + 1;
        }

        private int GetRight(int index)
        {
            return 2 * index + 2;
        }
    }
}
