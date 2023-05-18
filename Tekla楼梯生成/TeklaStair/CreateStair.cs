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
using Tekla.Structures.Geometry3d;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.Cryptography;

using System.Text.RegularExpressions;
using System.Windows.Forms;
using Point = Tekla.Structures.Geometry3d.Point;
using System.Drawing;
using Tekla.Structures.Drawing;
using Polygon = Tekla.Structures.Model.Polygon;
using Vector = Tekla.Structures.Geometry3d.Vector;

namespace TeklaStair
{
    public partial class CreateStair : Form
    {
        List<string> SectionList = GetHProfiles();


        public CreateStair()
        {
            
            InitializeComponent();
            Cmb_stair_type.SelectedIndex =0 ;
            
            Cmb_Section.DataSource = SectionList.Where(s => s.StartsWith("H")).ToList();
            
            Cmb_Section.SelectedIndex=0;

            List<string> Section_type = new List<string> { "H型钢", "C型钢", "F矩形钢管" };
            Cmb_Section_type.DataSource = Section_type;
            Cmb_Section_type.SelectedIndex = 0;





        }

        public static List<string> GetHProfiles()
        {
            List<Profile> hProfiles = new List<Profile>();
            //LibraryProfileItem correntProfileItem = null;
            //List<LibraryProfileItem> profileL = new List<LibraryProfileItem>();
            List<String> profileL_Name = new List<string>();
            CatalogHandler CatalogHandler = new CatalogHandler();
            ProfileItemEnumerator ProfileItemEnumerator = CatalogHandler.GetLibraryProfileItems();


            while (ProfileItemEnumerator.MoveNext())
            {
                LibraryProfileItem LibraryProfileItem = ProfileItemEnumerator.Current as LibraryProfileItem;

                string section_name = LibraryProfileItem.ProfileName;
                if (section_name.StartsWith("H") || section_name.StartsWith("C") || section_name.StartsWith("F"))
                    profileL_Name.Add(section_name); //将选中构件的截面汇入截面库profileL

            }


            //for (int j = 0; j < profileL.Count; j++)
            //{
            //    if (profileL[j].ProfileName.Contains(profileName))
            //    {
            //        correntProfileItem = profileL[j];//在截面库profileL中历遍寻找当截面的信息
            //    }
            //}
            return profileL_Name;


        }


        public void button1_Click(object sender, EventArgs e)
        {

            string f_path = @" Y:\数字化课题";


            if (Directory.Exists(f_path) == false)
            {
                f_path = @"C:\ProgramData\Autodesk\Revit\Addins\2018";
            }

            string section_name_string = Cmb_Section.SelectedItem.ToString();
            
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = " + f_path + @"\数据库\RevitData.db"))
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



                WorkPlaneHandler workPlaneHandler = myModelPlate.GetWorkPlaneHandler();
                TransformationPlane currentPlane = workPlaneHandler.GetCurrentTransformationPlane();


                Matrix transformationMatrix = currentPlane.TransformationMatrixToLocal;
                Point origin = transformationMatrix.Transform(new Point(0, 0, 0));
                Point axisX = transformationMatrix.Transform(new Point( 1, 0, 0));
                Point axisY = transformationMatrix.Transform(new Point(0, 1, 0));

                TransformationPlane oriPlane = new TransformationPlane(new CoordinateSystem(origin,new Vector( axisX- origin),new Vector(axisY- origin)));

                workPlaneHandler.SetCurrentTransformationPlane(oriPlane);



                string sqlPlate = "SELECT*FROM PlateInfo";
                DataTable Platetable = getData(sqlPlate, cmd);


                for (int x = 0; x < Platetable.Rows.Count; x++)
                {
                    string point_info = Platetable.Rows[x].ItemArray[2].ToString().Replace('(', ' ').Replace(')', ' ');
                    string plate_thick = Platetable.Rows[x].ItemArray[3].ToString();
                    string plate_name = Platetable.Rows[x].ItemArray[6].ToString();

                    //int np = Convert.ToInt32(point_info.Split('*')[0]);




                    if (plate_name == "构造钢筋")
                    {
                        //Generate_Rebar(point_info, plate_thick, myModelPlate);
                    }
                    else if (plate_name == "钢梁中心线")
                    {
                        Generate_Beam(point_info, section_name_string,myModelPlate);
                    }
                    else if (plate_name == "承接钢板")
                    {
                        if(Convert.ToDouble(point_info.Split('*')[0])>100)
                            Generate_fold_Plate(point_info, plate_thick, myModelPlate);
                        else
                            Generate_Plate(point_info, plate_thick, myModelPlate);
                    }
                    else if (plate_name == "底衬")
                    {

                    }
                    else if (plate_name == "平台钢板")
                    {

                    }
                    else if (plate_name == "平台悬挑")
                    {

                    }
                    else
                    {
                        Generate_Plate(point_info, plate_thick, myModelPlate);
                    }
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


        private static void Generate_fold_Plate(string point_info,string plate_thick, Model myModelPlate)
        {
            string p_len = point_info.Split('*')[0];

            List < string >L_s= point_info.Split('*').ToList();

            string L1 = L_s[1];
            string L2 = L_s[2];

            
            L1 = L1.Substring(1, L1.Length-2).Replace(" , ", "@");
            //L2 = L2.Substring(1, L2.Length-2);
            
            

            List<string> P_s = L1.Split('@').ToList();
            List<Point> PXYZ_s = new List<Point>();

            for (int i = 0; i < P_s.Count; i++)
            {
                double x_coord = Convert.ToDouble(P_s[i].Split(',')[0]);
                double y_coord = Convert.ToDouble(P_s[i].Split(',')[1]);
                double z_coord = Convert.ToDouble(P_s[i].Split(',')[2]);
                PXYZ_s.Add(new Point(x_coord, y_coord, z_coord));
            }

            // Sort the points by Z coordinate in ascending order
            PXYZ_s.Sort((p1, p2) => p1.Z.CompareTo(p2.Z));

            // Get the point with the maximum Z coordinate
            Point maxZPoint = PXYZ_s[2];
            PXYZ_s.RemoveAt(2);


            Point foldPoint = PXYZ_s[1];
            Point startPoint = PXYZ_s[0];

            Vector vert_check = new Vector(foldPoint - maxZPoint);
            if (vert_check.X > 1e-3)
            {
                foldPoint = PXYZ_s[0];
                startPoint = PXYZ_s[0];
            }

           
            ContourPoint point1 = new ContourPoint(startPoint,null);
            ContourPoint point2 = new ContourPoint(foldPoint,null);
            ContourPoint point3 = new ContourPoint(maxZPoint, null);


            PolyBeam PolyBeam = new PolyBeam();




            //if (Math.Abs(PXYZ_s[0].Z- PXYZ_s[1].Z)<1e-3)
            //{
            //    point1 = new ContourPoint(PXYZ_s[2], null);
            //    point2 = new ContourPoint(PXYZ_s[0], null);
            //    point3 = new ContourPoint(PXYZ_s[1], null);

            //}
            //else
            //{
            //    point1 = new ContourPoint(PXYZ_s[0], null);
            //    point2 = new ContourPoint(PXYZ_s[1], null);
            //    point3 = new ContourPoint(PXYZ_s[2], null);
            //    PolyBeam.Position.Rotation = Position.RotationEnum.BACK;
            //    PolyBeam.Position.Plane = Position.PlaneEnum.LEFT;
            //}





            PolyBeam.AddContourPoint(point1);
            PolyBeam.AddContourPoint(point2);
            PolyBeam.AddContourPoint(point3);

            PolyBeam.Profile.ProfileString = "PL"+p_len+"*"+plate_thick;
            PolyBeam.Material.MaterialString= "Q235B";

            PolyBeam.Position.Rotation = Position.RotationEnum.BACK;
            PolyBeam.Position.Plane = Position.PlaneEnum.LEFT;
            PolyBeam.Position.Depth = Position.DepthEnum.FRONT;
            PolyBeam.Finish = "PAINT";
            bool Result = false;
            Result = PolyBeam.Insert();



            myModelPlate.CommitChanges();
        }
        private static void Generate_Beam(string point_info,  string section_name_string, Model myModelPlate)
        {
            string ps = point_info.Split('*')[1];
            string pe = point_info.Split('*')[2];

            Point P1 = new Point(new Point(Convert.ToDouble(ps.Split(',')[0]), Convert.ToDouble(ps.Split(',')[1]),
                Convert.ToDouble(ps.Split(',')[2])));
            Point P2 = new Point(new Point(Convert.ToDouble(pe.Split(',')[0]), Convert.ToDouble(pe.Split(',')[1]),
                Convert.ToDouble(pe.Split(',')[2])));

            Beam Beam = new Beam(P1, P2);
            Beam.Profile.ProfileString = section_name_string;//"300-6-10*200";



            Beam.Material.MaterialString = "Q345B";
            //Beam.Finish = "PAINT";
            Beam.Insert();
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

            myModelPlate.CommitChanges();
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

        private void Cmb_stair_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_stair_type.Text.Contains("板式"))
            {
                Cmb_Section.Enabled = false;
                Cmb_Section_type.Enabled = false;


                string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Directory.SetCurrentDirectory(exePath);
                

                Image Img_Plate = Image.FromFile(exePath+"\\Plate_Stair.png");
                pictureBox1.Image = Img_Plate;

                Refresh();


            }
            else
            {
                Cmb_Section.Enabled = true;
                Cmb_Section_type.Enabled = true;

                string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Directory.SetCurrentDirectory(exePath);
                
                Image Img_Section = Image.FromFile(exePath + "\\Section_Stair.png");
                pictureBox1.Image = Img_Section;
                Refresh();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_Section_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmb_Section.DataSource = SectionList.Where(s => s.StartsWith(Cmb_Section_type.Text.Substring(0,1))).ToList();
        }



    }
}
