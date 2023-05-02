namespace TrainLikeAChampion
{
  public class Listing
  {
    public int ListingID { get; set; }
    public string TrainerName { get; set; }
    public int TrainerID { get; set; }
    public DateTime SessionDate { get; set; }
    public TimeSpan SessionTime { get; set; }
    public decimal SessionCost { get; set; }
    public bool IsTaken { get; set; }

    public override string ToString()
    {
      return $"{ListingID}#{TrainerName}#{SessionDate}#{SessionTime}#{SessionCost}#{IsTaken}";
    }

    public static Listing FromString(string line)
    {
      string[] parts = line.Split('#');
      return new Listing
      {
        ListingID = int.Parse(parts[0]),
        TrainerName = parts[1],
        SessionDate = DateTime.Parse(parts[2]),
        SessionTime = TimeSpan.Parse(parts[3]),
        SessionCost = decimal.Parse(parts[4]),
        IsTaken = bool.Parse(parts[5])
      };
    }
  }
}