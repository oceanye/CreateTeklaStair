using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

//using Excel = Microsoft.Office.Interop.Excel;
namespace Namespace
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class DataToSqlitePlate : IExternalCommand
    {
        public string DbPath;
        

        public void ClearTable(SQLiteCommand cmd)
        {
            cmd.CommandText = " delete from PlateInfo";
            cmd.ExecuteNonQuery();
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {


            UIDocument Uidoc = commandData.Application.ActiveUIDocument;
            Document Doc = Uidoc.Document;
            FamilySymbol fs = null;

            //FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            //folderBrowserDialog1.ShowDialog();
            //DbPath = folderBrowserDialog1.SelectedPath + @"\RevitData.db";

            DbPath = "Y://数字化课题//数据库"+ @"\RevitData.db";


            //IList<Element> ElementList = new FilteredElementCollector(Doc).OfClass(typeof(FamilyInstance)).ToElements();

            //IList<Element> CategoryList = new FilteredElementCollector(Doc).OfClass(typeof(FamilySymbol)).ToElements();

            //IList<Element> FloorList = new FilteredElementCollector(Doc).OfClass(typeof(Floor)).ToElements();

            //IList<Element> LevelList = new FilteredElementCollector(Doc).OfClass(typeof(Level)).ToElements();

            //IList<Element> OpenList = new FilteredElementCollector(Doc).OfClass(typeof(Opening)).ToElements();

            //IList<Element> GridList = new FilteredElementCollector(Doc).OfClass(typeof(Grid)).ToElements();

            //IList<Element> MultiGridList = new FilteredElementCollector(Doc).OfClass(typeof(MultiSegmentGrid)).ToElements();


            //List<FamilySymbol> ColumnList = new List<FamilySymbol>();
            //List<FamilySymbol> StrFrameList = new List<FamilySymbol>();




            FilteredElementCollector sybols = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilySymbol));
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            var columns = collector.OfClass(typeof(FamilyInstance));


            string info = "";








            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbPath))
            {
                try
                {


                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand();

                    cmd.Connection = conn;

                    ClearTable(cmd);//清空数据表



                    #region 楼板

                    //foreach (var ele in FloorList)
                    //{

                    //    Floor floor = Doc.GetElement(ele.Id) as Floor;

                    //    string str = floor.LookupParameter("注释").AsString();


                    //    List<GeometryObject> GeoSolid = new List<GeometryObject>();
                    //    Options geoOptions = new Options();
                    //    geoOptions.ComputeReferences = true;
                    //    GeometryElement Ge = ele.get_Geometry(geoOptions);

                    //    foreach (GeometryObject geo in Ge)
                    //    {
                    //        GeoSolid.Add(geo);
                    //    }

                    //    PlanarFace CompareYMax = null;

                    //    List<XYZ> pointList = new List<XYZ>();


                    //    #region 新方法
                    //    for (int goi = 0; goi < GeoSolid.Count; goi++)
                    //    {
                    //        if (goi == 0)
                    //        {

                    //            List<Face> arryList = new List<Face>();

                    //            #region 获取相应点
                    //            if ((GeoSolid[goi] as Solid) != null && (GeoSolid[goi] as Solid).Faces.Size > 0)
                    //            {
                    //                foreach (Face f in (GeoSolid[goi] as Solid).Faces)
                    //                {
                    //                    arryList.Add(f);
                    //                }
                    //                arryList = arryList.OrderByDescending(m => m.Area).ToList();


                    //                foreach (Face f in arryList)
                    //                {


                    //                    if ((f as PlanarFace).FaceNormal.Z > 0)
                    //                    {

                    //                        CompareYMax = f as PlanarFace;


                    //                        List<XYZ> list = CompareYMax.Triangulate().Vertices.ToList();

                    //                        list = list.OrderBy(m => m.X).ToList();
                    //                        pointList.Add(list[0]);
                    //                        pointList.Add(list[1]);
                    //                        pointList.Add(list[list.Count - 1]);
                    //                        pointList.Add(list[list.Count - 2]);

                    //                        pointList = pointList.OrderBy(m => m.Y).ToList();

                    //                        XYZ FinalX = pointList[0];
                    //                        XYZ FinalX2 = pointList[1];

                    //                        XYZ HFinalX1 = pointList[2];
                    //                        XYZ HFinalX2 = pointList[3];



                    //                        cmd.CommandText = "insert into SlabInfo(ID,Jt1Coord,Jt2Coord,Jt3Coord,Jt4Coord,thickness,DL,LL,LevelName) values(@ID,@Jt1Coord,@Jt2Coord,@Jt3Coord,@Jt4Coord,@thickness,@DL,@LL,@LevelName)";


                    //                        cmd.Parameters.AddWithValue("@ID", ele.Id.ToString());
                    //                        cmd.Parameters.AddWithValue("@Jt1Coord", "(" + Math.Round(UnitUtils.Convert(FinalX.X, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(FinalX.Y, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(FinalX.Z, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + ")");
                    //                        cmd.Parameters.AddWithValue("@Jt2Coord", "(" + Math.Round(UnitUtils.Convert(FinalX2.X, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(FinalX2.Y, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(FinalX2.Z, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + ")");
                    //                        cmd.Parameters.AddWithValue("@Jt3Coord", "(" + Math.Round(UnitUtils.Convert(HFinalX1.X, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(HFinalX1.Y, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(HFinalX1.Z, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + ")");
                    //                        cmd.Parameters.AddWithValue("@Jt4Coord", "(" + Math.Round(UnitUtils.Convert(HFinalX2.X, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(HFinalX2.Y, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + "," + Math.Round(UnitUtils.Convert(HFinalX2.Z, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)) + ")");

                    //                        cmd.Parameters.AddWithValue("@thickness", ele.LookupParameter("厚度").AsValueString());

                    //                        if (str != "" && str != null)
                    //                        {
                    //                            cmd.Parameters.AddWithValue("@DL", str.Split('=')[1].Split('/')[0].Substring(0, str.Split('=')[1].Split('/')[0].Length - 3));
                    //                            cmd.Parameters.AddWithValue("@LL", str.Split('=')[1].Split('/')[1].Substring(0, str.Split('=')[1].Split('/')[1].Length - 3));
                    //                        }
                    //                        else
                    //                        {
                    //                            cmd.Parameters.AddWithValue("@DL", "");
                    //                            cmd.Parameters.AddWithValue("@LL", "");
                    //                        }

                    //                        cmd.Parameters.AddWithValue("@LevelName", ele.LookupParameter("标高").AsValueString());

                    //                        cmd.ExecuteNonQuery();

                    //                    }
                    //                }
                    //            }
                    //            #endregion
                    //        }
                    //    }

                    //    #endregion


                    //}

                    #endregion

                    #region 拉伸体（楼梯）
                    foreach (FamilyInstance item in columns)
                    {
                        string id = item.Id.ToString();
                        string name = item.Symbol.FamilyName;
                        string syname = item.Symbol.Name;

                        string xyzs = null;

                        info += id + "," + name + "," + syname + "\r\n";

                        Options options = new Options();

                        GeometryElement geometry = item.get_Geometry(options);

                        int i = 0;

                        foreach (GeometryObject obj in geometry)
                        {
                            GeometryInstance instance = obj as GeometryInstance;

                            GeometryElement geometryElement = instance.GetInstanceGeometry();

                            foreach (GeometryObject elem in geometryElement)
                            {
                                Solid solid = elem as Solid;

                                //string gstyleName = gStyle.GraphicsStyleCategory.Name;

                                string gStyleId = "";
                                string gstyleName = "";

                                GraphicsStyle gStyle = Doc.GetElement(elem.GraphicsStyleId) as GraphicsStyle;
                                gStyleId = elem.GraphicsStyleId.ToString();

                                if (gStyle != null)
                                {
                                    gstyleName = gStyle.GraphicsStyleCategory.Name.ToString();
                                }
                                else
                                {
                                    continue;
                                }
                                if (solid != null)
                                {
                                    EdgeArray faceArray = solid.Edges;
                                    List<string> point_list = new List<string>();

                                    foreach (Edge ed in faceArray)
                                    {
                                        Curve cu = ed.AsCurve();
                                        if (cu != null)
                                        {
                                            XYZ xyz1 = cu.GetEndPoint(0);
                                            XYZ xyz2 = cu.GetEndPoint(1);
                                            //xyzs+=xyz1 +","+ xyz2;  
                                            point_list.Add(ConvertCoord2Mill(xyz1.ToString()));
                                            point_list.Add(ConvertCoord2Mill(xyz2.ToString()));
                                            //point_list.Add("("+  xyz1.X.ToString("f5")+","+ xyz1.Y.ToString("f5") + "," + xyz1.Y.ToString("f5")+")");
                                            //point_list.Add("(" + xyz2.X.ToString("f5") + "," + xyz2.Y.ToString("f5") + "," + xyz2.Y.ToString("f5") + ")");
                                            // Math.Round(UnitUtils.Convert(FinalX.X, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS)
                                        }

                                    }
                                    var point_list_distinct = point_list.Distinct().ToList();
                                    


                                    List<List<string>> Srf_info_list = CalSrfThk(point_list_distinct);

                                    double thk = Convert.ToDouble(Srf_info_list[0][0]);
                                    List<string> Srf_mid_point = new List<string>();
                                    for (int pi = 0; pi < Srf_info_list.Count(); pi = pi + 1)
                                    {
                                        Srf_mid_point.Add(Srf_info_list[pi][3]);
                                    }

                                    string point_str = Srf_mid_point.Count.ToString() +"*"+ string.Join("*", Srf_mid_point.ToArray());


                                    cmd.CommandText = "insert into PlateInfo(ID,FamilyName,CatalogName,PointCoord,thickness,detailId,detailName) values(@ID,@FamilyName,@CatalogName,@PointCoord,@thickness,@detailId,@detailName)";
                                    

                                    cmd.Parameters.AddWithValue("@ID", id);
                                    cmd.Parameters.AddWithValue("@FamilyName", name);
                                    cmd.Parameters.AddWithValue("@CatalogName", syname);
                                    cmd.Parameters.AddWithValue("@PointCoord", point_str);
                                    cmd.Parameters.AddWithValue("@thickness", Convert.ToString(Math.Round(thk)));
                                    cmd.Parameters.AddWithValue("@detailId", gStyleId);
                                    cmd.Parameters.AddWithValue("@detailName", gstyleName);

                                    cmd.ExecuteNonQuery();




                                }


                            }
                        }





                    }
                    conn.Close();
                    MessageBox.Show("完成");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion


            return Result.Succeeded;
        }

        private List<List<string>> CalSrfThk(List<string> point_list)//out double thk, out List<string>point_mid)
        {
            double thk = 1e5;
            List<List<string>> SrfInfo = new List<List<string>>();

            for (int i = 0; i < point_list.Count(); i = i + 1)
            {
                string p_i = point_list[i];
                for (int j = i + 1; j < point_list.Count(); j = j + 1)
                {
                    string p_j = point_list[j];
                    double d1 = CalDist(p_i, p_j);
                    //System.Diagnostics.Debug.WriteLine(Convert.ToString(d1) + ":" + Convert.ToString(thk));
                    if (d1 < thk * 1.05)
                    {
                        thk = d1;
                        List<String> info = new List<String>();
                        info.Add(d1.ToString());
                        info.Add(p_i);
                        info.Add(p_j);
                        SrfInfo.Add(info); // SrfInfo[i]=[dist,p1,p2]
                    }
                }

            }
            for (int i = 0; i < SrfInfo.Count(); i = i + 1)
            {
                if (Convert.ToDouble(SrfInfo[i][0]) < thk * 1.05)
                {
                    string midpoint = CalMidPoint(SrfInfo[i][1], SrfInfo[i][2]);
                    SrfInfo[i].Add(midpoint);
                }
                else
                {
                    SrfInfo[i] = null;
                }
            }

            RemoveNull(SrfInfo);


                return SrfInfo;
        }

        private double CalDist(string p1, string p2)
        {
            double p1x,p1y,p1z,p2x,p2y,p2z;
            var temp = p1.Substring(1, p1.Length - 1).Split(',');
            p1x =Convert.ToDouble(p1.Substring(1, p1.Length-2).Split(',')[0]);
            p1y = Convert.ToDouble(p1.Substring(1, p1.Length - 2).Split(',')[1]);
            p1z = Convert.ToDouble(p1.Substring(1, p1.Length - 2).Split(',')[2]);

            p2x = Convert.ToDouble(p2.Substring(1, p2.Length - 2).Split(',')[0]);
            p2y = Convert.ToDouble(p2.Substring(1, p2.Length - 2).Split(',')[1]);
            p2z = Convert.ToDouble(p2.Substring(1, p2.Length - 2).Split(',')[2]);

            double dist = Math.Sqrt((p1x - p2x) * (p1x - p2x) + (p1y - p2y) * (p1y - p2y) + (p1z - p2z) * (p1z - p2z));

            return dist;
        }

        private string CalMidPoint(string pa,string pb)
        {
            double pax, pay, paz, pbx, pby, pbz;
            pax = Convert.ToDouble(pa.Substring(1, pa.Length - 2).Split(',')[0]);
            pay = Convert.ToDouble(pa.Substring(1, pa.Length - 2).Split(',')[1]);
            paz = Convert.ToDouble(pa.Substring(1, pa.Length - 2).Split(',')[2]);

            pbx = Convert.ToDouble(pb.Substring(1, pb.Length - 2).Split(',')[0]);
            pby = Convert.ToDouble(pb.Substring(1, pb.Length - 2).Split(',')[1]);
            pbz = Convert.ToDouble(pb.Substring(1, pb.Length - 2).Split(',')[2]);

            string pm = "(" + (pax + pbx) / 2 + "," + (pay + pby) / 2 + "," + (paz + pbz) / 2 + ")";

            return pm;
        }

        static void RemoveNull<T>(List<T> list)
        {
            // 找出第一个空元素 O(n)
            int count = list.Count;
            for (int i = 0; i < count; i++)
                if (list[i] == null)
                {
                    // 记录当前位置
                    int newCount = i++;

                    // 对每个非空元素，复制至当前位置 O(n)
                    for (; i < count; i++)
                        if (list[i] != null)
                            list[newCount++] = list[i];

                    // 移除多余的元素 O(n)
                    list.RemoveRange(newCount, count - newCount);
                    break;
                }
        }

        static string ConvertCoord2Mill(string point)
        {
            double px,py,pz;
            string converted_point;
            px = UnitUtils.Convert(Convert.ToDouble(point.Substring(1, point.Length - 2).Split(',')[0]), DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS);
            py = UnitUtils.Convert(Convert.ToDouble(point.Substring(1, point.Length - 2).Split(',')[1]), DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS); 
            pz = UnitUtils.Convert(Convert.ToDouble(point.Substring(1, point.Length - 2).Split(',')[2]), DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS);

            converted_point = "("+ Convert.ToString(px)+","+ Convert.ToString(py) + ","+Convert.ToString(pz) + ")";
            return converted_point;
        }
    }
}
