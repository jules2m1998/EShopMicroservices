using Discount.Grpc;
using Dsicount.Grpc.Data;
using Dsicount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Dsicount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(
        CreateDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon =
            request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Invalid request object")
            );
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount is successfully created. ProductName : {productName}, Amount {amount}",
            coupon.ProductName,
            coupon.Amount
        );

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon =
            await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
            ?? throw new RpcException(
                new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName}")
            );

        dbContext.Coupons.Remove(coupon);
        var count = await dbContext.SaveChangesAsync();
        logger.LogInformation(
            "Discount is successfully deleted. ProductName : {productName}, Amount {amount}",
            coupon.ProductName,
            coupon.Amount
        );

        return new() { Success = count == 1 };
    }

    public override async Task<CouponModel> GetDiscount(
        GetDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = await dbContext.Coupons.SingleOrDefaultAsync(x =>
            x.ProductName == request.ProductName
        );
        coupon ??= new Coupon
        {
            ProductName = "No discount",
            Amount = 0,
            Description = "No discount Desc",
        };
        logger.LogInformation(
            "Discount is retrieved for ProductName : {productName}, Amount {amount}",
            coupon.ProductName,
            coupon.Amount
        );
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon =
            request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Invalid request object")
            );
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount is successfully updated. ProductName : {productName}, Amount {amount}",
            coupon.ProductName,
            coupon.Amount
        );

        return coupon.Adapt<CouponModel>();
    }
}
