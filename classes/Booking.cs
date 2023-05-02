namespace TrainLikeAChampion{
  class Booking{
    //Properties getters and setters
    public int SessionID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string TrainingDate { get; set; }
    public string TrainerID { get; set; }
    public string TrainerName { get; set; }
    public bool SessionAvailability { get; set; } // if Availability!="true", then the session is booked
  }
}