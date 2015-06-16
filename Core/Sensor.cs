using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GuiComponentInterfaces;
using System.Runtime.Serialization;
using System.Windows;

namespace Core
{
    /// <summary>
    /// Sensor class has data sent on client connect. This class is deserialized from JSON data.
    /// It belongs to some ClientObject.
    /// </summary>
    [DataContract]
    public class Sensor
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String name { get; set; }

        public bool SensorClicked { get; set; }

        [DataMember]
        public string description;

        private DataType _typeVal;
        public DataType TypeVal
        {
            get { return _typeVal; }
            set { _typeVal = value; }
        }

        [DataMember(Name = "dataType")]
        public string type
        {
            get
            {
                return _typeVal.ToString();
            }
            set
            {
                this._typeVal = (DataType)Enum.Parse(typeof(DataType), value, true);
            }
        }

        public IGuiComponent GuiComponent { get; set; }
        public int DataSeriesId { get; set; }

        /// <summary>
        /// Pushes certain dataValue to the GuiComponent.
        /// </summary>
        /// <param name="val"></param>
        public void AddValue(DataValue val)
        {
            if (GuiComponent == null)
            {
                Console.WriteLine("Passing data : " + val.Value.ToString() + ", but component is null!!!");
                return;
            }

            val.DataSeriesId = DataSeriesId;
            val.Type = TypeVal;

            Application.Current.Dispatcher.Invoke(new Action(() => {
                GuiComponent.AddValue(val);
            }));
        }

        /// <summary>
        /// Sets state of the Sensor as on and off. This is used to send proper message to Client
        /// ( wether it should send data about sensor or not ).
        /// </summary>
        public void Toggle()
        {
            if (SensorClicked == true)
            {
                SensorClicked = false;
            }
            else
            {
                SensorClicked = true;
            }
        }
    }
}
