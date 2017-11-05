using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Tests
{
    [TestClass()]
    public class TodoItemTests
    {
        [TestMethod()]
        public void TodoItemTest()
        {
            TodoItem todoItem = new TodoItem("item1");

            Assert.AreEqual(todoItem.Text, "item1");
            Assert.IsNotNull(todoItem.DateCreated);
            Assert.IsNull(todoItem.DateCompleted);
        }

        [TestMethod()]
        public void MarkAsCompletedTest()
        {
            TodoItem todoItem = new TodoItem("item1");

            Assert.IsNull(todoItem.DateCompleted);

            todoItem.MarkAsCompleted();

            Assert.IsNotNull(todoItem.DateCompleted);
        }

        [TestMethod()]
        public void ReplaceAllDataTest()
        {
            TodoItem originalItem = new TodoItem("original");
            TodoItem modifiedItem = new TodoItem("modified");
            modifiedItem.MarkAsCompleted();
            Guid id_before = originalItem.Id;

            originalItem.ReplaceAllData(modifiedItem);

            Assert.AreEqual(id_before, originalItem.Id);
            Assert.AreEqual(modifiedItem.DateCompleted, originalItem.DateCompleted);
            Assert.AreEqual(modifiedItem.DateCreated, originalItem.DateCreated);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            TodoItem item = new TodoItem("item");
            TodoItem item2 = new TodoItem("item2");
            item2.Id = item.Id;

            Assert.IsFalse(item.Equals(null));
            Assert.IsFalse(item.Equals(new TodoItem("item")));
            Assert.IsTrue(item.Equals(item2));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Guid guid = Guid.NewGuid();
            TodoItem item = new TodoItem("item");
            item.Id = guid;

            Assert.AreEqual(guid.GetHashCode(), item.GetHashCode());
        }
    }
}