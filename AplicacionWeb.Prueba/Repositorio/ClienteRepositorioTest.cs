using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionWeb.Modelo;
using AplicacionWeb.Repositorio;
using Xunit;
using FluentAssertions;
using System.Data.Entity;
using Moq;


namespace AplicacionWeb.Prueba.Repositorio
{
    public class ClienteRepositorioTest
    {
        private IRepositorio<Customer> repositorio;
        public ClienteRepositorioTest()
        {
            repositorio = new BaseRepositorio<Customer>();
        }

        [Fact(DisplayName = "AddTestWrongWithMissingData")]
        public void AddTestWrongWithMissingData()
        {
            var cliente = new Customer();
            cliente.FirstName = "Test";
            cliente.LastName = "Test";
            cliente.City = "Test";
            cliente.Phone = "12312312";
            try
            {
                repositorio.Agregar(cliente);
            }
            catch (Exception exception)
            {
                exception.Source.Should().Be("EntityFramework");
            }
        }

        [Fact(DisplayName = "AddTestWrongWithNull")]
        public void AddTestWrongWithNull()
        {
            var person = new Customer();
            try
            {
                repositorio.Agregar(person);
            }
            catch (Exception exception)
            {
                exception.Should().NotBeNull();
            }
        }

        [Fact(DisplayName = "AddTestWithProperData")]
        public void AddTestWithProperData()
        {
            var cliente = TestPersonOk();
            var result = repositorio.Agregar(cliente);
            result.Should().BeGreaterThan(0);
        }


        [Fact(DisplayName = "MockData")]
        public void MockData()
        {
            var personDbSetMock =
                new Mock<DbSet<Customer>>();

            var webContextMock =
                new Mock<ContextoWebDb>();

            webContextMock.Setup(m => m.Customer).
                Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Customer>()).
                Returns(personDbSetMock.Object);

            var repositorio =
                new BaseRepositorio<Customer>(webContextMock.Object);
            var newPerson = TestPersonOk();
            repositorio.Agregar(newPerson);
            personDbSetMock.Verify(p => p.Add(It.IsAny<Customer>()),
                                        Times.Once);
            webContextMock.Verify(w => w.SaveChanges(),
                                       Times.Once);

        }


        [Fact(DisplayName = "MockDataList")]
        public void MockDataList()
        {
            var personList = PersonList().AsQueryable();
            var personDbSetMock = new Mock<DbSet<Customer>>();
            personDbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(personList.Provider);

            personDbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(personList.Expression);

            personDbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(personList.ElementType);

            personDbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(personList.GetEnumerator());

            var webContextMock =
                new Mock<ContextoWebDb>();

            webContextMock.Setup(m => m.Customer).
                Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Customer>()).
                Returns(personDbSetMock.Object);

            var repositorio =
                new BaseRepositorio<Customer>(webContextMock.Object);

            var personGetByID = repositorio
                            .ObtenerId(p => p.FirstName == "Name1");
            personGetByID.Should().NotBeNull();

        }

        private IEnumerable<Customer> PersonList()
        {
            return Enumerable.Range(1, 10)
                .Select(i =>
                new Customer
                {
                    Id = i,
                    FirstName = $"Name{i}",
                    LastName = $"LastName{i}",
                    City = $"City{i}",
                    Phone = $"Phone{i}",
                });
        }
        private Customer TestPersonOk()
        {
            var cliente = new Customer();
            cliente.FirstName = "Test";
            cliente.LastName = "Test";
            cliente.City = "Test";
            cliente.Phone = "123123";
            return cliente;
        }
    }
}
