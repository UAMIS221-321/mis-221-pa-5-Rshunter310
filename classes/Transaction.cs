namespace TrainLikeAChampion
{
  public class Transaction
  {
    public int SessionID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime TrainingDate { get; set; }
    public int TrainerID { get; set; }
    public string TrainerName { get; set; }
    public decimal SessionCost { get; set; }
    public string Status { get; set; }

    public Transaction() { }

    public Transaction(int sessionId, string customerName, string customerEmail, DateTime trainingDate, int trainerId, string trainerName, decimal cost, string status)
    {
      SessionID = sessionId;
      CustomerName = customerName;
      CustomerEmail = customerEmail;
      TrainingDate = trainingDate;
      TrainerID = trainerId;
      TrainerName = trainerName;
      SessionCost = cost;
      Status = status;
    }
    public static Transaction FromString(string input)
    {
      string[] parts = input.Split('#');

      int sessionId = int.Parse(parts[0]);
      string customerName = parts[1];
      string customerEmail = parts[2];
      DateTime trainingDate = DateTime.Parse(parts[3]);
      int trainerId = int.Parse(parts[4]);
      string trainerName = parts[5];
      decimal cost = decimal.Parse(parts[6]);
      string status = parts[7];

      return new Transaction(sessionId, customerName, customerEmail, trainingDate, trainerId, trainerName, cost, status);
    }
  }
}