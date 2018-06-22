using System;
using System.Collections.Generic;
using System.Windows.Controls;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;

using Wind.Types;
using Wind.Utilities;
using Wind.Containers;

using Parrot.Containers;
using Parrot.Controls;
using Parrot.Windows;

using MahApps.Metro.Controls;
using Xceed.Wpf.Toolkit;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Parrot_GH.Utilities
{
    public class ReturnValues : GH_Component
    {
        List<string> keys = new List<string>();

        /// <summary>
        /// Initializes a new instance of the ReturnValues class.
        /// </summary>
        public ReturnValues()
          : base("Return Values", "Get", "---", "Aviary", "Dashboard Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Element to Listen to", GH_ParamAccess.tree);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Values", "V", "Values from the listener", GH_ParamAccess.tree);
            pManager.AddTextParameter("X", "X", "X", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string ID = this.Attributes.InstanceGuid.ToString();
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)), false).Text;
            List<string> newKeys = new List<string>();

            string type, format;
            GH_Structure<IGH_Goo> El;
            GH_Structure<IGH_Goo> OutPut = new GH_Structure<IGH_Goo>();

            if (!DA.GetDataTree(0, out El)) return;

            for (int i = 0; i < El.Paths.Count; i++)
            {
                GH_Path Q = El.get_Path(i);
                GH_Path P = El.get_Path(i);
                P.AppendElement(0);
                for (int k = 0; k < El.get_Branch(Q).Count; k++)
                {
                    IGH_Goo X = El.get_DataItem(Q, k);


                    wObject W;
                    X.CastTo(out W);

                    format = W.Type;

                    if (format == "Parrot")
                    {
                        pElement E;
                        E = (pElement)W.Element;

                        type = E.Type;
                        newKeys.Add(E.Element.Name);

                        switch (type)
                        {
                            case ("Button"):
                                Button C0 = (Button)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C0.PreviewMouseDown -= (o, e) => {ExpireSolution(true);};
                                    C0.PreviewMouseDown += (o, e) => {ExpireSolution(true);};

                                    C0.PreviewMouseUp -= (o, e) => { ExpireSolution(true); };
                                    C0.PreviewMouseUp += (o, e) => { ExpireSolution(true); };
                                }

                                OutPut.Append(new GH_ObjectWrapper((Mouse.LeftButton == MouseButtonState.Pressed) & C0.IsMouseOver), P);
                                break;
                            case ("Calculator"):
                                Calculator C1 = (Calculator)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C1.ValueChanged -= (o, e) => { ExpireSolution(true); };
                                    C1.ValueChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C1.Value), P);
                                break;
                            case ("Calendar"):
                                Calendar C2 = (Calendar)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C2.SelectedDatesChanged -= (o, e) => { ExpireSolution(true); };
                                    C2.SelectedDatesChanged += (o, e) => { ExpireSolution(true); };
                                }
                                List<IGH_Goo> Dates = new List<IGH_Goo>();

                                for (int j = 0; j < C2.SelectedDates.Count; j++)
                                {
                                    OutPut.Append(new GH_ObjectWrapper(C2.SelectedDates[j]), P);
                                }
                                break;
                            case ("Clock"):
                                Clock C23 = (Clock)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C23.LayoutUpdated -= (o, e) => { ExpireSolution(true); };
                                    C23.LayoutUpdated += (o, e) => { ExpireSolution(true); };
                                }
                                    OutPut.Append(new GH_ObjectWrapper(C23.Time), P);
                                break;
                            case ("CheckBox"):
                                CheckBox C19 = (CheckBox)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C19.Click -= (o, e) => { ExpireSolution(true); };
                                    C19.Click += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C19.IsChecked), P);
                                break;
                            case ("CheckListBox"):
                                CheckListBox C3 = (CheckListBox)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C3.ItemSelectionChanged -= (o, e) => { ExpireSolution(true); };
                                    C3.ItemSelectionChanged += (o, e) => { ExpireSolution(true); };
                                }
                                for (int j = 0; j < C3.SelectedItems.Count; j++)
                                {
                                    OutPut.Append(new GH_ObjectWrapper(C3.SelectedItems[j]), P);
                                }
                                break;
                             case ("ColorCanvas"):
                                 ColorCanvas C4 = (ColorCanvas)E.Element;
                                 if (!keys.Contains(E.Element.Name))
                                 {
                                     C4.SelectedColorChanged -= (o, e) => { ExpireSolution(true); };
                                     C4.SelectedColorChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(new wColor((System.Windows.Media.Color)C4.SelectedColor).ToDrawingColor()), P);
                                OutPut.Append(new GH_ObjectWrapper(C4.SelectedColor), P);
                                 break;
                             case ("ColorPicker"):
                                 ColorPicker C5 = (ColorPicker)E.Element;
                                 if (!keys.Contains(E.Element.Name))
                                 {
                                     C5.SelectedColorChanged -= (o, e) => { ExpireSolution(true); };
                                     C5.SelectedColorChanged += (o, e) => { ExpireSolution(true); };
                                 }
                                OutPut.Append(new GH_ObjectWrapper(new wColor((System.Windows.Media.Color)C5.SelectedColor).ToDrawingColor()), P);
                                 break;
                            case ("CheckComboBox"):
                                CheckComboBox C6 = (CheckComboBox)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C6.ItemSelectionChanged -= (o, e) => { ExpireSolution(true); };
                                    C6.ItemSelectionChanged += (o, e) => { ExpireSolution(true); };
                                }
                                for (int j = 0; j < C6.SelectedItems.Count; j++)
                                {
                                    OutPut.Append(new GH_ObjectWrapper(C6.SelectedItems[j]), P);
                                }
                                break;
                            case ("RangeSlider"):
                                MahApps.Metro.Controls.RangeSlider C7 = (MahApps.Metro.Controls.RangeSlider)E.Layout.Children[0];
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C7.UpperValueChanged -= (o, e) => { ExpireSolution(true); };
                                    C7.UpperValueChanged += (o, e) => { ExpireSolution(true); };
                                    C7.LowerValueChanged -= (o, e) => { ExpireSolution(true); };
                                    C7.LowerValueChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(new Interval(C7.LowerValue, C7.UpperValue)), P);
                                break;
                            case ("Slider"):
                                Slider C8 = (Slider)E.Layout.Children[0];
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C8.ValueChanged -= (o, e) => { ExpireSolution(true); };
                                    C8.ValueChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C8.Value), P);
                                break;
                            case ("RadioButton"):
                                RadioButton C9 = (RadioButton)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C9.Checked -= (o, e) => { ExpireSolution(true); };
                                    C9.Checked += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C9.IsChecked), P);
                                break;
                            case ("TimePicker"):
                                MaterialDesignThemes.Wpf.TimePicker C10 = (MaterialDesignThemes.Wpf.TimePicker)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C10.MouseUp -= (o, e) => { ExpireSolution(true); };
                                    C10.MouseUp += (o, e) => { ExpireSolution(true); };
                                    C10.LayoutUpdated -= (o, e) => { ExpireSolution(true); };
                                    C10.LayoutUpdated += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C10.SelectedTime), P);
                                break;
                            case ("PickDate"):
                                DatePicker C11 = (DatePicker)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C11.SelectedDateChanged -= (o, e) => { ExpireSolution(true); };
                                    C11.SelectedDateChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C11.SelectedDate), P);
                                break;
                            case ("PickDateTime"):
                                DateTimePicker C12 = (DateTimePicker)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C12.ValueChanged -= (o, e) => { ExpireSolution(true); };
                                    C12.ValueChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C12.Value), P);
                                break;
                            case ("ListView"):
                                ListView C13 = (ListView)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C13.SelectionChanged -= (o, e) => { ExpireSolution(true); };
                                    C13.SelectionChanged += (o, e) => { ExpireSolution(true); };
                                }
                                for (int j = 0; j < C13.Items.Count; j++)
                                {
                                    Label tbox = (Label)C13.Items[j];
                                    
                                    OutPut.Append(new GH_ObjectWrapper(tbox.ToolTip.ToString()), P);
                                }
                                break;
                            case ("ListBox"):
                                ListBox C14 = (ListBox)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C14.SelectionChanged -= (o, e) => { ExpireSolution(true); };
                                    C14.SelectionChanged += (o, e) => { ExpireSolution(true); };
                                }
                                for (int j = 0; j < C14.SelectedItems.Count; j++)
                                {
                                    OutPut.Append(new GH_ObjectWrapper(C14.SelectedItems[j]), P);
                                }
                                break;
                            case ("ComboBox"):
                                ComboBox C15 = (ComboBox)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C15.SelectionChanged -= (o, e) => { ExpireSolution(true); };
                                    C15.SelectionChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C15.SelectedValue), P);
                                break;
                            case ("ScrollValue"):
                                ButtonSpinner C16 = (ButtonSpinner)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C16.Spin -= (o, e) => { ExpireSolution(true); };
                                    C16.Spin += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C16.Content), P);
                                break;
                            case ("ScrollNumber"):
                                NumericUpDown C17 = (NumericUpDown)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C17.ValueChanged -= (o, e) => { ExpireSolution(true); };
                                    C17.ValueChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C17.Value), P);
                                break;
                            case ("TextBox"):
                                TextBox C18 = (TextBox)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C18.TextChanged -= (o, e) => { ExpireSolution(true); };
                                    C18.TextChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C18.Text), P);
                                break;
                            case ("TreeView"):
                                TreeView C20 = (TreeView)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C20.SelectedItemChanged -= (o, e) => { ExpireSolution(true); };
                                    C20.SelectedItemChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C20.SelectedValue), P);
                                break;
                            case ("GridView"):
                                ListView C21 = (ListView)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C21.SelectionChanged -= (o, e) => { ExpireSolution(true); };
                                    C21.SelectionChanged += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C21.Items), P);
                                break;
                            case ("Toggle"):
                                ToggleButton C22 = (ToggleButton)E.Element;
                                if (!keys.Contains(E.Element.Name))
                                {
                                    C22.Click -= (o, e) => { ExpireSolution(true); };
                                    C22.Click += (o, e) => { ExpireSolution(true); };
                                }
                                OutPut.Append(new GH_ObjectWrapper(C22.IsChecked), P);
                                break;
                        }
                    }
                }
            }

            keys = newKeys;

            DA.SetDataTree(0, OutPut);
            DA.SetDataList(1, keys);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Return_Value;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{d1b5f08d-dc5b-47cf-859b-0b248631f24e}"); }
        }
    }
}