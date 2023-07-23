namespace EdrakTask.Tests.Core.Services
{
    public class CustomerServiceTest
    {
        private StubOrderRepository _repository;
        private IMapper _mapper;
        private ICustomerService _customerService;

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
            _customerService= new CustomerService(_mapper, _repository);
        }

        [Test]
        public void GetOrder_Done()
        {
            // Arrange
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
            };
            var order = new Order()
            {
                CustomerId = customer.Id
            };
            _repository._orders.Add(order);
            _repository._customers.Add(customer);
            // Act
            var orders = _customerService.GetOrders(customer.Id);

            // Assert
            Assert.IsTrue(orders.Count() == 1);
        }
    }
}
