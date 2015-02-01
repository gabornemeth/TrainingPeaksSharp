using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TrainingPeaksSharp.Extensions;

namespace TrainingPeaksSharp
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ushort AvgHeartRate { get; set; }
        public ushort MaxHeartRate { get; set; }
        public ushort MinHeartRate { get; set; }
        public string Type { get; set; }
        public long ElapsedTime { get; set; }
        public short AvgPower { get; set; }
        public short MaxPower { get; set; }
        public string Description { get; set; }

        public Workout(XElement xmlElement)
        {
            Id = xmlElement.GetFirstDescendantValue<int>("WorkoutId");
            Description = xmlElement.GetFirstDescendantValue<string>("Description");
            Day = xmlElement.GetFirstDescendantValue<DateTime>("WorkoutDay");
            Type = xmlElement.GetFirstDescendantValue<string>("WorkoutTypeDescription");
            ElapsedTime = xmlElement.GetFirstDescendantValue<int>("TimeTotalInSeconds");
            AvgHeartRate = xmlElement.GetFirstDescendantValue<ushort>("HeartRateAverage");
            MinHeartRate = xmlElement.GetFirstDescendantValue<ushort>("HeartRateMinimum");
            MaxHeartRate = xmlElement.GetFirstDescendantValue<ushort>("HeartRateMaximum");
            AvgPower = xmlElement.GetFirstDescendantValue<short>("AvgPower");
            MaxPower = xmlElement.GetFirstDescendantValue<short>("MaxPower");

            //            <WorkoutId>90044725</WorkoutId>
            //<WorkoutDay>2012-03-22T00:00:00</WorkoutDay>
            //<WorkoutTypeDescription>Bike</WorkoutTypeDescription>
            //<PlannedDistanceInMeters xsi:nil="true"/>
            //<DistanceInMeters>73401.8</DistanceInMeters>
            //<PowerAverage>205</PowerAverage>
            //<PowerMaximum>504</PowerMaximum>
            //<TimeTotalInSeconds>11060</TimeTotalInSeconds>
            //<PlannedTimeTotalInSeconds xsi:nil="true"/>
            //<VelocityMaximum>18.768</VelocityMaximum>
            //<VelocityAverage>6.637</VelocityAverage>
            //<CadenceMaximum>177</CadenceMaximum>
            //<CadenceAverage>69</CadenceAverage>
            //<HeartRateMaximum>179</HeartRateMaximum>
            //<HeartRateMinimum>74</HeartRateMinimum>
            //<HeartRateAverage>145</HeartRateAverage>
            //<StartTime>2012-03-22T16:11:50</StartTime>
            //<ElevationGain>1642</ElevationGain>
            //<ElevationLoss>1639</ElevationLoss>
            //<ElevationMinimum>28</ElevationMinimum>
            //<ElevationMaximum>1380.4000244140625</ElevationMaximum>
            //<ElevationAverage>534.86201804487871</ElevationAverage>
            //<WorkoutCode>Custom</WorkoutCode>
            //<StartTimePlanned xsi:nil="true"/>
        }
    }
}
