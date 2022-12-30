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
                    string point_info = Platetable.Rows[x].ItemArray[2].ToString();
                    string plate_thick = Platetable.Rows[x].ItemArray[3].ToString();

                    int np = Convert.ToInt32(point_info.Split('*')[0]);


                    if (np == 3)
                    {
                        string p1 = point_info.Split('*')[1].Replace('(',' ').Replace(')',' ');
                        string p2 = point_info.Split('*')[2].Replace('(', ' ').Replace(')', ' ');
                        string p3 = point_info.Split('*')[3].Replace('(', ' ').Replace(')', ' ');
                        Tekla.Structures.Geometry3d.Point Pt1 = new Tekla.Structures.Geometry3d.Point(Convert.ToDouble(p1.Split(',')[0]), Convert.ToDouble(p1.Split(',')[1]), Convert.ToDouble(p1.Split(',')[2]) );
                        Tekla.Structures.Geometry3d.Point Pt2 = new Tekla.Structures.Geometry3d.Point(Convert.ToDouble(p2.Split(',')[0]), Convert.ToDouble(p2.Split(',')[1]), Convert.ToDouble(p2.Split(',')[2]) );
                        Tekla.Structures.Geometry3d.Point Pt3 = new Tekla.Structures.Geometry3d.Point(Convert.ToDouble(p3.Split(',')[0]), Convert.ToDouble(p3.Split(',')[1]), Convert.ToDouble(p3.Split(',')[2]) );
                        ContourPoint point = new ContourPoint(Pt1, null);
                        ContourPoint point1 = new ContourPoint(Pt2, null);
                        ContourPoint point2 = new ContourPoint(Pt3, null);

                        ContourPlate CP = new ContourPlate();
                        CP.AddContourPoint(point);
                        CP.AddContourPoint(point1);
                        CP.AddContourPoint(point2);

                        //CP.Finish = "FOO";
                        CP.Profile.ProfileString = "PL" + plate_thick;
                        CP.Material.MaterialString = "Q235B";
                        CP.Insert();
                        myModelPlate.CommitChanges();
                        //CP.Delete;

                    }
                    else if (np==4)
                    {
                        string p1 = point_info.Split('*')[1].Replace('(', ' ').Replace(')', ' ');
                        string p2 = point_info.Split('*')[2].Replace('(', ' ').Replace(')', ' ');
                        string p3 = point_info.Split('*')[3].Replace('(', ' ').Replace(')', ' ');
                        string p4 = point_info.Split('*')[4].Replace('(', ' ').Replace(')', ' ');
                        Tekla.Structures.Geometry3d.Point Pt1 = new Tekla.Structures.Geometry3d.Point(Convert.ToDouble(p1.Split(',')[0]), Convert.ToDouble(p1.Split(',')[1]), Convert.ToDouble(p1.Split(',')[2]));
                        Tekla.Structures.Geometry3d.Point Pt2 = new Tekla.Structures.Geometry3d.Point(Convert.ToDouble(p2.Split(',')[0]), Convert.ToDouble(p2.Split(',')[1]), Convert.ToDouble(p2.Split(',')[2]));
                        Tekla.Structures.Geometry3d.Point Pt3 = new Tekla.Structures.Geometry3d.Point(Convert.ToDouble(p3.Split(',')[0]), Convert.ToDouble(p3.Split(',')[1]), Convert.ToDouble(p3.Split(',')[2]));
                        Tekla.Structures.Geometry3d.Point Pt4 = new Tekla.Structures.Geometry3d.Point(Convert.ToDouble(p4.Split(',')[0]), Convert.ToDouble(p4.Split(',')[1]), Convert.ToDouble(p4.Split(',')[2]));
                        
                        ContourPoint point = new ContourPoint(Pt1, null);
                        ContourPoint point1 = new ContourPoint(Pt2, null);
                        ContourPoint point2 = new ContourPoint(Pt3, null);
                        ContourPoint point3 = new ContourPoint(Pt4, null);

                        ContourPlate CP = new ContourPlate();
                        CP.AddContourPoint(point);
                        CP.AddContourPoint(point1);
                        CP.AddContourPoint(point2);
                        CP.AddContourPoint(point3);

                        //CP.Finish = "FOO";
                        CP.Profile.ProfileString = "PL" + plate_thick;
                        CP.Material.MaterialString = "Q235B";

                        CP.Insert();
                        myModelPlate.CommitChanges();
                    }
                    
                }
                    


     




                #endregion




                conn.Close();


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
