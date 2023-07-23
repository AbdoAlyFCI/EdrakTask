using EdrakTask.Core.Enums;

namespace EdrakTask.Tests.Core.Services
{
    public class OrderServiceTest
    {
        private StubOrderRepository _repository;
        private IMapper _mapper;
        private IOrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _repository = new StubOrderRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderLine, OrderLineDto>();
            });
            _mapper = config.CreateMapper();
            _orderService = new OrderService(_mapper, _repository);
        }

        [Test]
        public void GetOrders_CountIsNull_ReturnAllOrders()
        {
            // Arrange
            _repository._orders.Add(new Order());
            _repository._orders.Add(new Order());
            _repository._orders.Add(new Order());

            // Act
            var orders = _orderService.GetOrders(null);

            // Assert
            Assert.IsTrue(orders.Count() == _repository._orders.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void GetOrders_CountNotNull_ReturnSameOrderCount(int count)
        {
            // Arrange
            _repository._orders.Add(new Order());
            _repository._orders.Add(new Order());
            _repository._orders.Add(new Order());
            _repository._orders.Add(new Order());
            _repository._orders.Add(new Order());

            // Act
            var orders = _orderService.GetOrders(count);

            // Assert
            Assert.IsTrue(orders.Count() == count);
        }

        [Test]
        public void GetOrder_OrderIdIsEmpty_ThrowArgumentNullException()
        {
            // Act
            void action() => _orderService.GetOrder(Guid.Empty);

            // Assert
            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void GetOrder_OrderIdNotEmpty_GetSameOrder()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repository._orders.Add(new Order() { Id = id });

            // Act
            var order = _orderService.GetOrder(id);

            // Assert
            Assert.NotNull(order);
        }

        [Test]
        public void UpdateOrder_OrderIdIsEmpty_ThrowArgumentNullException()
        {
            // Act
            void action() => _orderService.UpdateOrder(Guid.Empty, OrderStatusEnum.Pending);

            // Assert
            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        [TestCase(OrderStatusEnum.Shipped)]
        [TestCase(OrderStatusEnum.Delivered)]
        [TestCase(OrderStatusEnum.Pending)]
        public void UpdateOrder_OrderIdNotNull_Update(OrderStatusEnum orderStatus)
        {
            // Arrange
            var id = Guid.NewGuid();
            var order = new Order() { Id = id };
            _repository._orders.Add(order);

            // Act
            _orderService.UpdateOrder(id, orderStatus);

            // Assert
            Assert.IsTrue(order.StatusId == (int)orderStatus);
        }

        [Test]
        public void OrderExist_OrderIdIsEmpty_ThrowArgumentNullException()
        {
            // Act
            void action() => _orderService.OrderExist(Guid.Empty);

            // Assert
            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void OrderExist_OrderNotEmpty_ReturnFalse()
        {
            // Act
            bool result = _orderService.OrderExist(Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void OrderExist_OrderNotEmpty_ReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repository._orders.Add(new Order() { Id = id });

            // Act
            bool result = _orderService.OrderExist(id);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CreateOrder_ArgumentIsNull_ThrowArgumentNullException()
        {
            // Act
            void action() => _orderService.CreateOrder(null);

            // Assert
            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void CreateOrder_CustomerNotExist_ThrowEntityNotFoundException()
        {
            // Arrange 
            var orderInput = new OrderInputDto()
            {
                CustomerId = Guid.NewGuid()
            };

            // Act
            void action() => _orderService.CreateOrder(orderInput);

            // Assert
            Assert.Throws<EntityNotFoundException>(() => action());
        }

        [Test]
        public void CreateOrder_NotAllProductExist_ThrowEntityNotFoundException()
        {
            // Arrange
            var prouduct1 = new Product() { Id = Guid.NewGuid() };
            var prouduct2 = new Product() { Id = Guid.NewGuid() };
            var prouduct3 = new Product() { Id = Guid.NewGuid() };

            _repository._products.Add(prouduct1);
            _repository._products.Add(prouduct2);
            _repository._products.Add(prouduct3);

            var customer = new Customer() { Id = Guid.NewGuid() };
            _repository._customers.Add(customer);

            var orderInput = new OrderInputDto()
            {
                CustomerId = customer.Id,
                OrderLines = new List<OrderLineInputDto>()
                {
                    new OrderLineInputDto()
                    {
                        ProductId = prouduct1.Id
                    },
                    new OrderLineInputDto()
                    {
                        ProductId = prouduct2.Id
                    },
                    new OrderLineInputDto()
                    {
                        ProductId = Guid.NewGuid()
                    }
                }
            };

            // Act
            void action() => _orderService.CreateOrder(orderInput);

            // Assert
            Assert.Throws<EntityNotFoundException>(() => action());
        }

        [Test]
        public void CreateOrder_QunatityMoreThanAvailble_ThrowInvalidOperationException()
        {
            // Arrange
            var prouduct1 = new Product() { Id = Guid.NewGuid(), Quantity = 4 };

            _repository._products.Add(prouduct1);
            var customer = new Customer() { Id = Guid.NewGuid() };
            _repository._customers.Add(customer);

            var orderInput = new OrderInputDto()
            {
                CustomerId = customer.Id,
                OrderLines = new List<OrderLineInputDto>()
                {
                    new OrderLineInputDto()
                    {
                        ProductId = prouduct1.Id,
                        Amount = 5
                    },
                }
            };

            // Act
            void action() => _orderService.CreateOrder(orderInput);

            // Assert
            Assert.Throws<InvalidOperationException>(() => action());
        }

        [Test]
        public void CreateOrder_ValidData_Pass()
        {
            // Arrange
            var prouduct1 = new Product() { Id = Guid.NewGuid(), Quantity = 4 };

            _repository._products.Add(prouduct1);
            var customer = new Customer() { Id = Guid.NewGuid() };
            _repository._customers.Add(customer);

            var orderInput = new OrderInputDto()
            {
                CustomerId = customer.Id,
                OrderLines = new List<OrderLineInputDto>()
                {
                    new OrderLineInputDto()
                    {
                        ProductId = prouduct1.Id,
                        Amount = 4
                    },
                }
            };

            // Act
            _orderService.CreateOrder(orderInput);

            // Assert
            Assert.That(_repository._orders.Count.Equals(1));
        }

        [Test]
        public void CancelOrder_OrderIdIsEmpty_ThrowArgumentNullException()
        {
            // Act
            void action() => _orderService.CancelOrder(Guid.Empty);

            // Assert
            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void CancelOrder_validData_Pass()
        {
            // Arrange
            var product = new Product() { Id = Guid.NewGuid(), Quantity = 3 };
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Quantity = 4,
                        Product = product,
                        ProductId = product.Id
                    }
                }
            };
            _repository._products.Add(product);
            _repository._orders.Add(order);

            // Act
            _orderService.CancelOrder(order.Id);

            // Assert
            Assert.That(_repository._orders.Count.Equals(0));
            Assert.That(_repository._products.FirstOrDefault().Quantity.Equals(7));

        }

    }
}

