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
using System.Numerics;

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

            List<string> Obj_List = new List<string>{ "三角钢板", "封边钢板", "承接钢板" ,"钢梁中心线", "构造钢筋" };

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








            
            
                try
                {

                    SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbPath);
                   
                    var cmd = Sql_Clean_Data(conn);


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
                                gStyleId = elem.GraphicsStyleId.ToString(); // detailed id



                                if (gStyle != null)
                                {
                                    gstyleName = gStyle.GraphicsStyleCategory.Name.ToString();// detailedName
                                }
                                else
                                {
                                    continue;
                                }

                                if (Obj_List.Contains(gstyleName)==false)
                                    continue;

                                if (gstyleName == "钢梁中心线")
                                {
                                    Line beam_line = elem as Line;

                                    Extract_Beam_Gemo(conn,beam_line,id,name,syname, gStyleId, gstyleName);
                                    continue;
                                }

                                if (gstyleName == "构造钢筋")
                                {
                                    Line beam_line = elem as Line;

                                    Extract_Rebar_Gemo(conn, solid, id, name, syname, gStyleId, gstyleName);
                                    continue;
                            }

                            // 摘录实体solid

                            if (solid != null)
                                {
                                    Extract_Solid_Gemo(conn,solid, id, name, syname, gStyleId, gstyleName);
                                }


                            }
                        }





                    }
                    
                    MessageBox.Show("完成");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
            #endregion


            return Result.Succeeded;
        }

        private void Extract_Solid_Gemo(SQLiteConnection conn, Solid solid, string id, string name, string syname, string gStyleId, string gstyleName)
        {
            EdgeArray eArray = solid.Edges;
            FaceArray fArray = solid.Faces;
            List<string> point_list = new List<string>();
            double thk = 0;
            string point_str = "";

            foreach (Edge ed in eArray)
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

            if (point_list_distinct.Count < 0)
            {
                List<List<string>> Srf_info_list = CalSrfThk(point_list_distinct);

                thk = Convert.ToDouble(Srf_info_list[0][0]);
                List<string> Srf_mid_point = new List<string>();
                for (int pi = 0; pi < Srf_info_list.Count(); pi = pi + 1)
                {
                    Srf_mid_point.Add(Srf_info_list[pi][3]);
                }

                point_str = Srf_mid_point.Count.ToString() + "*" + string.Join("*", Srf_mid_point.ToArray());
            }
            else if (point_list_distinct.Count <= 8 && point_list_distinct.Count > 2)
            {
                List<XYZ> fCorner = new List<XYZ>();
                Face f_side = null;
                double f_area = 0;
                foreach (Face f in fArray)
                {
                    if (f.Area > f_area) // 三角形或四边形，选取最大面积为拉伸基础面，并后续计算厚度
                    {
                        f_area = f.Area;
                        f_side = f;
                    }
                }

                EdgeArrayArray eLoopArray = f_side.EdgeLoops;
                foreach (EdgeArray eloopA1 in eLoopArray)
                {
                    foreach (Edge eloop1 in eloopA1)
                    {
                        List<string> fCornerS = ConvertXYZ2String(fCorner);

                        if (fCornerS.Contains(Convert.ToString(eloop1.Tessellate()[0])) == false)
                            fCorner.Add(eloop1.Tessellate()[0]);
                        if (fCornerS.Contains(Convert.ToString(eloop1.Tessellate()[1])) == false)
                            fCorner.Add(eloop1.Tessellate()[1]);
                    }
                }

                fCorner = point_sort_CW(fCorner);


                // 计算厚度


                foreach (string p in point_list_distinct)

                {
                    string p1 = p.Replace('(', ' ').Replace(')', ' ');
                    XYZ pt = new XYZ(
                        UnitUtils.Convert(Convert.ToDouble(p1.Split(',')[0]), DisplayUnitType.DUT_MILLIMETERS,
                            DisplayUnitType.DUT_DECIMAL_FEET),
                        UnitUtils.Convert(Convert.ToDouble(p1.Split(',')[1]), DisplayUnitType.DUT_MILLIMETERS,
                            DisplayUnitType.DUT_DECIMAL_FEET),
                        UnitUtils.Convert(Convert.ToDouble(p1.Split(',')[2]), DisplayUnitType.DUT_MILLIMETERS,
                            DisplayUnitType.DUT_DECIMAL_FEET));
                    thk = dist_point2srf(fCorner[0], fCorner[1], fCorner[2], pt);
                    if (thk > 1e-3)
                        break;
                }


                //求得中心面
                List<string> Srf_mid_point2 = new List<string>();

                Srf_mid_point2 = CalSrfThk_with_Srf(f_side, fArray, thk);
                point_str = Srf_mid_point2.Count.ToString() + "*" + string.Join("*", Srf_mid_point2.ToArray());
            }
            //根据节点数等于多边形数，筛选出拉伸基础面的复杂多边形顺序点位
            else if (point_list_distinct.Count > 8)
            {
                List<XYZ> fCorner = new List<XYZ>();
                Face f_side = null;
                foreach (Face f in fArray)
                {
                    EdgeArrayArray eLoopArray = f.EdgeLoops;
                    fCorner = new List<XYZ>(); // 每次点位清零

                    foreach (EdgeArray eloopA1 in eLoopArray)
                    {
                        foreach (Edge eloop1 in eloopA1)
                            fCorner.Add(eloop1.Tessellate()[0]);
                    }

                    if (fCorner.Count == point_list_distinct.Count / 2)
                    {
                        if (f != null)
                        {
                            f_side = f;
                            break;
                        }
                    }
                }


                // 计算厚度


                foreach (string p in point_list_distinct)

                {
                    string p1 = p.Replace('(', ' ').Replace(')', ' ');
                    XYZ pt = new XYZ(
                        UnitUtils.Convert(Convert.ToDouble(p1.Split(',')[0]), DisplayUnitType.DUT_MILLIMETERS,
                            DisplayUnitType.DUT_DECIMAL_FEET),
                        UnitUtils.Convert(Convert.ToDouble(p1.Split(',')[1]), DisplayUnitType.DUT_MILLIMETERS,
                            DisplayUnitType.DUT_DECIMAL_FEET),
                        UnitUtils.Convert(Convert.ToDouble(p1.Split(',')[2]), DisplayUnitType.DUT_MILLIMETERS,
                            DisplayUnitType.DUT_DECIMAL_FEET));
                    thk = dist_point2srf(fCorner[0], fCorner[1], fCorner[2], pt);
                    if (thk > 1e-3)
                        break;
                }


                //求得中心面
                List<string> Srf_mid_point2 = new List<string>();

                Srf_mid_point2 = CalSrfThk_with_Srf(f_side, fArray, thk);
                point_str = Srf_mid_point2.Count.ToString() + "*" + string.Join("*", Srf_mid_point2.ToArray());
            }


            //

            Sql_Write_Data(conn,id, name, syname, point_str, thk, gStyleId, gstyleName);
        }


        private void Extract_Beam_Gemo(SQLiteConnection conn, Line BeamLine, string id, string name, string syname, string gStyleId, string gstyleName)
        {
            string point_str = "";
            double thk = 0;

            string p_s = ConvertCoord2Mill(BeamLine.Tessellate()[0].ToString());
            string p_e = ConvertCoord2Mill(BeamLine.Tessellate()[1].ToString());

            //
            point_str = "2*" + p_s+"*"+p_e;
            Sql_Write_Data(conn,id, name, syname, point_str, thk, gStyleId, gstyleName);
        }


        private void Extract_Rebar_Gemo(SQLiteConnection conn, Solid solid, string id, string name, string syname, string gStyleId, string gstyleName)
        {
            string point_str = "";
            double thk = 0;
            List<XYZ> p_list = new List<XYZ>();

            FaceArray F_array = solid.Faces;
            foreach (Face f in F_array)
            {
                PlanarFace planarFace = f as PlanarFace;
                if (null != planarFace)
                {
                    p_list.Add(planarFace.Origin);
                }
            }

            double l0 = CalDist(p_list[0].ToString(), p_list[1].ToString());
            thk = Math.Round(UnitUtils.Convert(Math.Sqrt(solid.Volume / l0 / Math.PI * 4), DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS), 0);

            string p_s = ConvertCoord2Mill(p_list[0].ToString());
            string p_e = ConvertCoord2Mill(p_list[1].ToString());

            //
            point_str = "2*" + p_s + "*" + p_e;
            Sql_Write_Data(conn, id, name, syname, point_str, thk, gStyleId, gstyleName);
        }

        private static void Sql_Write_Data(SQLiteConnection conn,string id, string name, string syname, string point_str, double thk, string gStyleId,
            string gstyleName)
        {
            
            SQLiteCommand cmd =new SQLiteCommand();
            conn.Open();
            cmd.Connection = conn;

            cmd.CommandText =
                "insert into PlateInfo(ID,FamilyName,CatalogName,PointCoord,thickness,detailId,detailName) values(@ID,@FamilyName,@CatalogName,@PointCoord,@thickness,@detailId,@detailName)";


            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@FamilyName", name);
            cmd.Parameters.AddWithValue("@CatalogName", syname);
            cmd.Parameters.AddWithValue("@PointCoord", point_str);
            cmd.Parameters.AddWithValue("@thickness", Convert.ToString(Math.Round(thk)));
            cmd.Parameters.AddWithValue("@detailId", gStyleId);
            cmd.Parameters.AddWithValue("@detailName", gstyleName);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private SQLiteCommand Sql_Clean_Data(SQLiteConnection conn)
        {
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Connection = conn;

            ClearTable(cmd); //清空数据表
            conn.Close();
            return cmd;
        }

        /// <summary>
        /// 根据点位，计算配对的两点中点，并筛选最短距离即为厚度方向，寻找匹配的点位，求的中点坐标
        /// </summary>
        /// <param name="point_list"></param>
        /// <returns></returns>
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


                return SrfInfo;// [0]--厚度 ;[1][2]起始终点; [3]midpoint
        }


        private List<string> CalSrfThk_with_Srf(Face f1,FaceArray fArray,double thk0)//out double thk, out List<string>point_mid)
        {
            List<string> Srfmid = new List<string>();

            foreach (EdgeArray eloop1 in f1.EdgeLoops)
            {
                foreach (Edge e1 in eloop1)
                {
                    string ps = ConvertCoord2Mill(Convert.ToString(e1.Tessellate()[0]));
                    int Flag = 0;
                    foreach (Face f in fArray)
                    {

                        foreach (EdgeArray eloop in f.EdgeLoops)
                        {
                            foreach (Edge e in eloop)
                            {
                                Curve curve_edge = e.AsCurve();
                                double d0 = Math.Abs(UnitUtils.Convert(curve_edge.Length, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS));
                                string p1 = ConvertCoord2Mill(Convert.ToString(curve_edge.Tessellate()[0]));
                                string p2 = ConvertCoord2Mill(Convert.ToString(curve_edge.Tessellate()[1]));

                                double select_edge = Math.Abs(CalDist(p1, ps) * CalDist(p2, ps));

                                if (select_edge < 1e-3)
                                    Flag++;
                                    if (Math.Abs(d0 - thk0) < 0.1)
                                        {

                                            string midpoint = CalMidPoint(p1, p2);
                                            if (Srfmid.Contains(midpoint) == false)
                                                Srfmid.Add(midpoint);
                                        }
                            }
                        }
                    }
                }
            }



            return Srfmid;
        }


        //求点到平面的距离
        //已知3点坐标，求平面ax+by+cz+d=0;

        double dist_point2srf(XYZ p1, XYZ p2, XYZ p3,XYZ pt)
        {
            double a;
            double b;
            double c;
            double d;
            double dist;

            a = (p2.Y - p1.Y) * (p3.Z - p1.Z) - (p2.Z - p1.Z) * (p3.Y - p1.Y);

            b = (p2.Z - p1.Z) * (p3.X - p1.X) - (p2.X - p1.X) * (p3.Z - p1.Z);

            c = (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);

            d = 0 - (a * p1.X + b * p1.Y + c * p1.Z);

            if ((a * a + b * b + c * c)==0)
            
                dist = 0;
            else
                dist = Math.Abs(a * pt.X + b * pt.Y + c * pt.Z + d) / Math.Sqrt(a * a + b * b + c * c);

            //return dist;
            return UnitUtils.Convert(dist, DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS); 
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

            string pm = "(" + Math.Round((pax + pbx) / 2,3) + "," + Math.Round((pay + pby) / 2,3) + "," + Math.Round((paz + pbz) / 2,3) + ")";

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
            px = UnitUtils.Convert(Math.Round(Convert.ToDouble(point.Substring(1, point.Length - 2).Split(',')[0]),10), DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS);
            py = UnitUtils.Convert(Math.Round(Convert.ToDouble(point.Substring(1, point.Length - 2).Split(',')[1]),10), DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS); 
            pz = UnitUtils.Convert(Math.Round(Convert.ToDouble(point.Substring(1, point.Length - 2).Split(',')[2]),10), DisplayUnitType.DUT_DECIMAL_FEET, DisplayUnitType.DUT_MILLIMETERS);

            converted_point = "("+ Convert.ToString(px)+","+ Convert.ToString(py) + ","+Convert.ToString(pz) + ")";
            return converted_point;
        }


        static List<string> ConvertXYZ2String(List<XYZ>fCorner)
        {
            List<string> fCornerS = new List<string>();
            foreach (XYZ p in fCorner)
            {
                fCornerS.Add(Convert.ToString(p));
            }

            return fCornerS;
        }

        static List<XYZ> point_sort_CW(List<XYZ> pointList)
        {
            //中点作为参考点
            double Xm = 0;
            double Ym = 0;
            double Zm = 0;
            int N = pointList.Count;

            foreach (XYZ p in pointList)
            {
                Xm = Xm + p.X;
                Ym = Ym + p.Y;
                Zm = Zm + p.Z;
            }

            XYZ pm = new XYZ(Xm / N,Ym/N,Zm/N);


            List<Vector3> vectorList = new List<Vector3>();
            List<double> angleList = new List<double>();
            //Vector3 v1 = new Vector3((float)(pointList[0].X - pm.X), (float)(pointList[0].Y - pm.Y), (float)(pointList[0].Z - pm.Z));

            //vectorList.Add(v1);

            //形成中点到各个点的向量
            foreach(XYZ p in pointList)
            {
                Vector3 vp = new Vector3((float)(p.X - pm.X), (float)(p.Y - pm.Y), (float)(p.Z - pm.Z));
                vectorList.Add(vp);
            }
            //计算向量1与各个向量的夹角 (0,2pi)

            Vector3 v1 = vectorList[0];
            Vector3 v2 = vectorList[1];
            foreach (Vector3 vp in vectorList)
                angleList.Add(vector_angle_2pi(v1,v2, vp));

            //按角度进行排序


            for (int i = 0; i < pointList.Count - 1; i++)  //外层循环控制排序趟数
            {
                for (int j = 0; j < pointList.Count - 1 - i; j++)  //内层循环控制每一趟排序多少次
                {
                    if (angleList[j] > angleList[j + 1])
                    {
                        var temp = pointList[j];
                        pointList[j] = pointList[j + 1];
                        pointList[j + 1] = temp;
                    }
                }
            }

            return pointList;
        }

        static double vector_angle_2pi(Vector3 v1,Vector3 v2,Vector3 v3)//求v1，v3夹角；方向参考v1->v2，同向为正，反向为负
        {
            double angle_2pi;

            double angle = Math.Acos(Math.Round(Vector3.Dot(v1, v3) / (v1.Length() * v3.Length()),3));//此处round,避免出现大于1的奇异值

            //double angle2 = Math.Acos(1);
            Vector3 dir_v1_v2 = Vector3.Cross(v1, v2);
            Vector3 dir_v1_v3 = Vector3.Cross(v1, v3);

            if (Vector3.Dot(dir_v1_v2, dir_v1_v3) < 0) //V1->V2 逆时针为正
                angle_2pi = 2 * Math.PI - angle;
            else
                angle_2pi = angle;
            //计算theta = arccos()
                
                //计算叉乘向量,并调整theta
            return angle_2pi/Math.PI*180;
        }
    }
}
