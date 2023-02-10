using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository discountRepository, IMapper mapper) 
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _discountRepository =discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var result = await _discountRepository.GetDiscount(request.ProductName);

            if (result == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Product Name - {request.ProductName} not found."));
            }

            _logger.LogInformation($"Discount retrieved for ProductName {result.ProductName}, Amount {result.Amount} ");
                
            return _mapper.Map<CouponModel>(result);

        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var result = await _discountRepository.CreateDiscount(coupon);

            _logger.LogInformation($"Discount Created for ProductName {coupon.ProductName}, Amount {coupon.Amount} ");

            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var result = await _discountRepository.UpdateDiscount(coupon);

            _logger.LogInformation($"Discount Updated for ProductName {coupon.ProductName}, Amount {coupon.Amount} ");

            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _discountRepository.DeleteDiscount(request.ProductName);

            var response = new DeleteDiscountResponse
            {
                Success = result,
            };

            return response;
        }
    }
}
