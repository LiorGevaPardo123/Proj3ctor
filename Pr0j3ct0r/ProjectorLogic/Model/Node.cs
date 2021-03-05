using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Model
{
    public class Node<T>
    {
        private T info;
        public T Info
        {
            get { return info; }
            set { info = value; }
        }

        private ICollection<Node<T>> next;
        public ICollection<Node<T>> Next
        {
            get { return next; }
            set { next = value; }
        }
    }
}