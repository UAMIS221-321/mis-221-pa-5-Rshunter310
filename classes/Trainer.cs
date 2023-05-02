using System;
using System.Collections.Generic;
using System.IO;

namespace TrainLikeAChampion
{
  public class Trainer
  {
    public int TrainerID { get; set; }
    public string TrainerName { get; set; }
    public string MailingAddress { get; set; }
    public string EmailAddress { get; set; }

    public override string ToString()
    {
      return $"{TrainerID}#{TrainerName}#{MailingAddress}#{EmailAddress}";
    }

    public static Trainer FromString(string line)
    {
      string[] parts = line.Split('#');
      return new Trainer
      {
        TrainerID = int.Parse(parts[0]),
        TrainerName = parts[1],
        MailingAddress = parts[2],
        EmailAddress = parts[3]
      };
    }
  }
}