using System;
using System.Data;
using System.Collections.Generic;

namespace TripleSoftware.NeoRhythm.Data
{

    public enum ChartInterval{
        TwoWeeks,
        SevenDays
    }

	/// <summary>
	/// Description of BiorhythmCalculator.
	/// </summary>
	public class BiorhythmCalculator
	{
		private const int physical = 23;
		private const int emotional = 28;
		private const int intellectual = 33;

        private ChartInterval interval = ChartInterval.TwoWeeks;

		private DateTime currentDate;
		private DateTime birthDate;
		
        public DateTime BirthDate {
			get { 
                return birthDate; 
            }
			set { 
                birthDate = value;
                FireUpdate();
            }
		}

		public DateTime CurrentDate {
			get { 
                return currentDate; 
            }
			set { 
                currentDate = value;
                FireUpdate();
            }
		}

        public event EventHandler Updated;

        private void FireUpdate()
        {
            if (Updated != null)
                Updated(this, EventArgs.Empty);
        }

        public int DaysSinceBirth{
			get {
                TimeSpan timeBetween = currentDate - birthDate;
                return (timeBetween.Days + 1);
            }
		}
		
		public double Physical{
			get { return CalculateRhithm( physical, DaysSinceBirth); }
		}
		
		public double Emotional{
			get { return CalculateRhithm( emotional, DaysSinceBirth); }
		}

		public double Intellactual{
			get { return CalculateRhithm( intellectual, DaysSinceBirth); }
		}

        public int[] GetPhysical()
        { 
            List<int> result = new List<int>();

            //if(interval == ChartInterval.TwoWeeks){
                for(int i = -7; i <= 7; i++){
                    double rhytm = CalculateRhithm(physical, (this.DaysSinceBirth + i));
                    int point = Convert.ToInt32((Math.Round(rhytm, 0) + 100) / 2);
                    result.Add(point);
                }
            //}

            return result.ToArray();
        }

        public int[] GetEmotional()
        {
            List<int> result = new List<int>();

            //if(interval == ChartInterval.TwoWeeks){
            for (int i = -7; i <= 7; i++) {
                double rhytm = CalculateRhithm(emotional, (this.DaysSinceBirth + i));
                int point = Convert.ToInt32((Math.Round(rhytm, 0) + 100) / 2);
                result.Add(point);
            }
            //}

            return result.ToArray();
        }

        public int[] GetIntellactual()
        {
            List<int> result = new List<int>();

            //if(interval == ChartInterval.TwoWeeks){
            for (int i = -7; i <= 7; i++) {
                double rhytm = CalculateRhithm(intellectual, (this.DaysSinceBirth + i));
                int point = Convert.ToInt32((Math.Round(rhytm, 0) + 100) / 2);
                result.Add(point);
            }
            //}

            return result.ToArray();
        }

		private double CalculateRhithm(int periode, int days){

			return (Math.Sin((2*Math.PI*Convert.ToDouble(days))/Convert.ToDouble(periode))*100);
		}
		
		public BiorhythmCalculator()
		{
			currentDate = DateTime.Now;	
		}


	}
}