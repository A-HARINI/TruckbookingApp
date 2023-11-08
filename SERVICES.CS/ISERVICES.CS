using Truck1.Model;

namespace Truck1;
public interface IServices{
 public List<Confirmedbookedtruckdetail> GetMonthlyConfirmedBookings(int year, int month);
 public List<Confirmedbookedtruckdetail> GetDailyConfirmedBookings(DateTime date);
 public ResponseDTO  GetAvailableTruckFacilitiesWithHighLoadCapacity(List<TruckfacilityDTO> truckfacilities);
 public ResponseDTO GetConfirmedBookedTruckDetailsWithConvenienceRequested();
 public FeedbackCountsDTO GetFeedbackCounts();
 public List<string> ValidateOfferDetailDTO(OfferdetailDTO offerDetailDTO);
 public List<OfferdetailDTO> GetOfferDetailsForSpecificMonth(int targetMonth, int targetYear);
 public Customer FindCustomerWithMostTruckBookings();
  
}