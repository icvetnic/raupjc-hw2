using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {

        [TestMethod()]
        public void GetTest()
        {
            TodoItem item1 = new TodoItem("item1");
            TodoRepository todoRepo = new TodoRepository();

            Assert.IsNull(todoRepo.Get(Guid.NewGuid()));

            todoRepo.Add(item1);

            Assert.AreEqual(item1, todoRepo.Get(item1.Id));
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddDuplicatesTest()
        {
            TodoRepository todRepo = new TodoRepository();
            TodoItem item1 = new TodoItem("item1");

            Assert.AreEqual(item1, todRepo.Add(item1));
            todRepo.Add(item1);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            TodoRepository todoRepo = new TodoRepository();
            TodoItem item1 = new TodoItem("item1");

            Assert.IsFalse(todoRepo.Remove(item1.Id));

            todoRepo.Add(item1);

            Assert.IsTrue(todoRepo.Remove(item1.Id));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            TodoRepository todoRepo = new TodoRepository();
            TodoItem item1 = new TodoItem("item1");
            string text = "item1_textchange";

            Assert.AreEqual(item1, todoRepo.Update(item1));

            item1.MarkAsCompleted();
            item1.Text = text;

            todoRepo.Update(item1);

            TodoItem changedItem = todoRepo.Get(item1.Id);

            Assert.AreEqual(text, changedItem.Text);
            Assert.IsTrue(changedItem.IsCompleted);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MarkAsCompletedTest()
        {
            TodoRepository todoRepo = new TodoRepository();
            TodoItem item1 = new TodoItem("item1");
            todoRepo.Add(item1);

            Assert.IsTrue(todoRepo.MarkAsCompleted(item1.Id));

            todoRepo.MarkAsCompleted(Guid.NewGuid());
        }

        [TestMethod()]
        public void GetAllTest()
        {
            TodoItem item1 = new TodoItem("item1")
            {
                DateCreated = new DateTime(2017, 11, 4)
            };
            TodoItem item2 = new TodoItem("item2")
            {
                DateCreated = new DateTime(2017, 11, 3)
            };
            TodoItem item3 = new TodoItem("item3")
            {
                DateCreated = new DateTime(2017, 11, 2)
            };
            TodoItem item4 = new TodoItem("item4")
            {
                DateCreated = new DateTime(2017, 11, 1)
            };

            List<TodoItem> orderedItems = new List<TodoItem>()
            {
                item1, item2, item3, item4
            };

            IGenericList<TodoItem> todoitems = new GenericList<TodoItem>()
            {
                item4, item1, item2, item3
            };
            TodoRepository todoRepo = new TodoRepository(todoitems);

            Assert.IsTrue(orderedItems.SequenceEqual(todoRepo.GetAll()));
        }

        [TestMethod()]
        public void GetActiveAndCompetedTest()
        {
            TodoItem item1 = new TodoItem("item1");
            TodoItem item2 = new TodoItem("item2");
            TodoItem item3 = new TodoItem("item3");
            TodoItem item4 = new TodoItem("item4");

            item1.MarkAsCompleted();
            item4.MarkAsCompleted();
            List<TodoItem> activeItems = new List<TodoItem>()
            {
                item2, item3
            };

            List<TodoItem> completedItems = new List<TodoItem>()
            {
                item1, item4
            };

            IGenericList<TodoItem> todoitems = new GenericList<TodoItem>()
            {
                item1, item2, item3, item4
            };
            TodoRepository todoRepo = new TodoRepository(todoitems);

            Assert.IsTrue(activeItems.SequenceEqual(todoRepo.GetActive()));
            Assert.IsTrue(completedItems.SequenceEqual(todoRepo.GetCompleted()));
        }

        [TestMethod()]
        public void GetFilteredTest()
        {
            TodoItem itemAa = new TodoItem("item_aa");
            TodoItem itemAb = new TodoItem("item_ab");
            TodoItem itemCa = new TodoItem("item_ca");
            TodoItem itemCb = new TodoItem("item_cb");

            List<TodoItem> meetsFilter = new List<TodoItem>()
            {
                itemAa, itemCa
            };

            IGenericList<TodoItem> todoitems = new GenericList<TodoItem>()
            {
                itemAa, itemAb, itemCa, itemCb
            };
            TodoRepository todoRepo = new TodoRepository(todoitems);

            Assert.IsTrue(meetsFilter.SequenceEqual(todoRepo.GetFiltered(item => item.Text.EndsWith("a"))));
        }
    }
}