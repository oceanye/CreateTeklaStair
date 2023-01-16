using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Shapes;
using Tekla.Structures.Catalogs;
using System.Diagnostics;
using System.Threading;
using Tekla.Structures.Model;
using System.Collections;
using System.IO;
using Tekla.Structures.Geometry3d;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TeklaStair
{
    public partial class CreateStair : Form
    {
        public CreateStair()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string DbPath = @" Y:\数字化课题\数据库\RevitData.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = " + DbPath))
            {
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand();

                cmd.Connection = conn;

                //string sqlBeam = "SELECT*FROM PlateInfo";

                //DataTable profileBeamData = getData(sqlBeam, cmd);






                CatalogHandler CatalogHandler = new CatalogHandler();



                ProfileItemEnumerator ProfileItemEnumerator = CatalogHandler.GetLibraryProfileItems();
                MaterialItemEnumerator MaterialItemEn = CatalogHandler.GetMaterialItems();









                #region 删除原始轴网
                Model myModelGrid = new Model();

                List<Tekla.Structures.Model.Grid> objectToSelectGrid = new List<Tekla.Structures.Model.Grid>();


                ModelObjectEnumerator myEnumFitting = myModelGrid.GetModelObjectSelector().GetAllObjectsWithType(Tekla.Structures.Model.ModelObject.ModelObjectEnum.GRID);


                while (myEnumFitting.MoveNext())
                {
                    objectToSelectGrid.Add(myEnumFitting.Current as Tekla.Structures.Model.Grid);
                }


                foreach (Tekla.Structures.Model.Grid g in objectToSelectGrid)
                {
                    g.Delete();
                }

                myModelGrid.CommitChanges();


                #endregion

                #region 生成新轴网
                //cmd.CommandText = "SELECT*FROM LevelTable";

                //SQLiteDataAdapter adapterGrid = new SQLiteDataAdapter(cmd);
                //DataSet dsGrid = new DataSet();
                //adapterGrid.Fill(dsGrid);

                //DataTable tableGrid = dsGrid.Tables[0];
                //Model modelNewGrid = new Model();

                //Tekla.Structures.Model.Grid grid = new Tekla.Structures.Model.Grid();

                //string Z = "";

                //string LabelZ = "+0 ";

                //for (int i = 0; i < tableGrid.Rows.Count; i++)
                //{
                //    Z += tableGrid.Rows[i].ItemArray[2].ToString() + " ";

                //    LabelZ += tableGrid.Rows[i].ItemArray[1].ToString() + " ";
                //}


                //grid.Name = "Grid";
                //grid.CoordinateX = "0.00";
                //grid.CoordinateY = "0.00";
                //grid.CoordinateZ = Z;
                //grid.LabelX = "1";
                //grid.LabelY = "A";
                //grid.LabelZ = LabelZ;

                //grid.ExtensionLeftX = 2000;
                //grid.ExtensionLeftY = 2000;
                //grid.ExtensionLeftZ = 2000;

                //grid.ExtensionRightX = 2000;
                //grid.ExtensionRightY = 2000;
                //grid.ExtensionRightZ = 2000;

                //grid.IsMagnetic = true;

                //grid.Insert();

                //modelNewGrid.CommitChanges();

                #endregion



                #region 板


                Model myModelPlate = new Model();

                string sqlPlate = "SELECT*FROM PlateInfo";
                DataTable Platetable = getData(sqlPlate, cmd);


                for (int x = 0; x < Platetable.Rows.Count; x++)
                {
                    string point_info = Platetable.Rows[x].ItemArray[2].ToString().Replace('(', ' ').Replace(')', ' ');
                    string plate_thick = Platetable.Rows[x].ItemArray[3].ToString();
                    string plate_name = Platetable.Rows[x].ItemArray[6].ToString();

                    //int np = Convert.ToInt32(point_info.Split('*')[0]);


                    //Beam Beam = new Beam(new Point(5000, 7000, 0), new Point(6000, 7000, 0));
                    //Beam.Profile.ProfileString = "250*250";
                    //Beam.Material.MaterialString = "K40-1";
                    //Beam.Finish = "PAINT";
                    //Beam.Insert();

                    //double MinimumY = Beam.GetSolid().MinimumPoint.Y;
                    //double MinimumX = Beam.GetSolid().MinimumPoint.X;
                    //double MinimumZ = Beam.GetSolid().MinimumPoint.Z;
                    //double MaximumY = Beam.GetSolid().MaximumPoint.Y;
                    //double MaximumX = Beam.GetSolid().MaximumPoint.X;
                    //double MaximumZ = Beam.GetSolid().MaximumPoint.Z;
                    if(plate_name == "构造钢筋")
                        Generate_Rebar(point_info, plate_thick,myModelPlate);
                    else
                        Generate_Plate(point_info, plate_thick, myModelPlate);
                }
                    


     




                #endregion




                conn.Close();


                

               



            }



        }

        private static void Generate_Rebar(string point_info,string plate_thick, Model myModelPlate)
        {
            string ps = point_info.Split('*')[1];
            string pe = point_info.Split('*')[2];

            Polygon Polygon = new Polygon();
            Polygon.Points.Add(new Point(Convert.ToDouble(ps.Split(',')[0]), Convert.ToDouble(ps.Split(',')[1]),
                Convert.ToDouble(ps.Split(',')[2])));
            Polygon.Points.Add(new Point(Convert.ToDouble(pe.Split(',')[0]), Convert.ToDouble(pe.Split(',')[1]),
                Convert.ToDouble(pe.Split(',')[2])));

            SingleRebar SingleRebar = new SingleRebar();
            SingleRebar.Polygon = Polygon;
            //SingleRebar.Father = Beam;
            SingleRebar.Name = "SingleRebar";
            SingleRebar.Class = 9;
            SingleRebar.Size = plate_thick;
            SingleRebar.NumberingSeries.StartNumber = 0;
            SingleRebar.NumberingSeries.Prefix = "Single";
            SingleRebar.Grade = "HRB400";
            //SingleRebar.OnPlaneOffsets = new ArrayList();
            //SingleRebar.OnPlaneOffsets.Add(25.00);
            //SingleRebar.StartHook.Angle = -90;
            //SingleRebar.StartHook.Length = 10;
            //SingleRebar.StartHook.Radius = 10;
            //SingleRebar.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            //SingleRebar.EndHook.Angle = 90;
            //SingleRebar.EndHook.Length = 10;
            //SingleRebar.EndHook.Radius = 10;
            //SingleRebar.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;

            SingleRebar.Insert();
            myModelPlate.CommitChanges();
        }

        private static void Generate_Plate(string point_info, string plate_thick, Model myModelPlate)
        {
            List<string> point_List = new List<string>();
            point_List = point_info.Split('*').ToList();
            if (Convert.ToDouble(point_List[0]) > 2)
            {
                point_List.RemoveAt(0);
                ContourPlate CP = new ContourPlate();


                foreach (string point_i in point_List)
                {
                    Tekla.Structures.Geometry3d.Point Pt_i = new Tekla.Structures.Geometry3d.Point(
                        Convert.ToDouble(point_i.Split(',')[0]), Convert.ToDouble(point_i.Split(',')[1]),
                        Convert.ToDouble(point_i.Split(',')[2]));
                    ContourPoint point_CP_i = new ContourPoint(Pt_i, null);
                    CP.AddContourPoint(point_CP_i);
                }

                CP.Profile.ProfileString = "PL" + plate_thick;
                CP.Material.MaterialString = "Q235B";
                CP.Insert();
                myModelPlate.CommitChanges();
            }
        }

        

        /// <summary>
        /// 获得数据库数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable getData(string sql, SQLiteCommand cmd)
        {

            cmd.CommandText = sql;

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            DataTable table = ds.Tables[0];

            return table;
        }

    }
}
