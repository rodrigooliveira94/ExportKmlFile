using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RodrigoDeveloper.Dominio.Entidades;

namespace RodrigoDeveloper.Dominio.Facades
{
    public class ExportKmlFile
    {
        public string name { get; set; }
        public StringBuilder xml { get; set; }
    }

    public class ExportKmlPolygon
    {
        public ExportKmlPolygonDocument ExportKmlPolygonDocuments { get; set; }
        public StringBuilder xml { get; set; }

        public StringBuilder GerarXml()
        {
            string id;
            xml = new StringBuilder();
            ExportKmlPolygonDocument document = null;
            if (ExportKmlPolygonDocuments != null)
                document = ExportKmlPolygonDocuments;

            if (document != null)
            {
                xml.Append("<?xml version='1.0' encoding='UTF-8'?>")
                    .Append("<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' ")
                    .Append("xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>")
                    .Append("<Document>")
                    .Append("<name>" + document.name + "</name>");

                #region Style/StyleMap
                if (document.ExportKmlPolygonStyles != null)
                {
                    foreach (var style in document.ExportKmlPolygonStyles)
                    {
                        id = style.id != null ? " id=\"" + style.id + "\"" : "";
                        xml.Append("<Style" + id + ">");

                        if (style.ExportKmlPolygonStyleIconStyles != null)
                        {
                            xml.Append("<IconStyle>")
                                .Append("<scale>" + style.ExportKmlPolygonStyleIconStyles.scale + "</scale>")
                                .Append("</IconStyle>");
                        }

                        if (style.ExportKmlPolygonStyleLineStyles != null)
                        {
                            xml.Append("<LineStyle>")
                                .Append("<color>" + style.ExportKmlPolygonStyleLineStyles.color + "</color>")
                                .Append("</LineStyle>");
                        }

                        if (style.ExportKmlPolygonStylePolyStyles != null)
                        {
                            xml.Append("<PolyStyle>");

                            if (style.ExportKmlPolygonStylePolyStyles.color != null)
                            {
                                xml.Append("<color>" + style.ExportKmlPolygonStylePolyStyles.color + "</color>");
                            }

                            if (style.ExportKmlPolygonStylePolyStyles.outline != null)
                            {
                                xml.Append("<outline>" + style.ExportKmlPolygonStylePolyStyles.outline + "</outline>");
                            }

                            xml.Append("</PolyStyle>");
                        }

                        xml.Append("</Style>");
                    }
                }

                if (document.ExportKmlPolygonStyleMaps != null)
                {
                    foreach (var styleMaps in document.ExportKmlPolygonStyleMaps)
                    {
                        id = styleMaps.id != null ? " id=\"" + styleMaps.id + "\"" : "";
                        xml.Append("<StyleMap" + id + ">");

                        if (styleMaps.ExportKmlPolygonStyleMapPairs != null)
                        {
                            foreach (var Pairs in styleMaps.ExportKmlPolygonStyleMapPairs)
                            {
                                xml.Append("<Pair>")
                                    .Append("<key>" + Pairs.key + "</color>")
                                    .Append("<styleUrl>" + Pairs.styleUrl +
                                            "</outline>")
                                    .Append("</Pair>");
                            }
                        }

                        xml.Append("</StyleMap>");
                    }
                }
                #endregion

                if (document.ExportKmlPolygonPlacemarks != null)
                {
                    foreach (var placemark in Enumerable.ToList(document.ExportKmlPolygonPlacemarks))
                    {
                        xml.Append("<Placemark>")
                            .Append("<name>" + placemark.name + "</name>")
                            .Append("<styleUrl>" + placemark.styleUrl + "</styleUrl>")
                            .Append("<LookAt>")
                            .Append("<longitude>" + placemark.ExportKmlPolygonPlacemarkLookAts.longitude + "</longitude>")
                            .Append("<latitude>" + placemark.ExportKmlPolygonPlacemarkLookAts.latitude + "</latitude>")
                            .Append("<altitude>" + placemark.ExportKmlPolygonPlacemarkLookAts.altitude + "</altitude>")
                            .Append("<gx:altitudeMode>" + placemark.ExportKmlPolygonPlacemarkLookAts.gxAltitudeMode + "</gx:altitudeMode>")
                            .Append("<range>" + placemark.ExportKmlPolygonPlacemarkLookAts.range + "</range>")
                            .Append("</LookAt>")
                            .Append("<Polygon>")
                            .Append("<tessellate>" + placemark.ExportKmlPolygonPlacemarkPolygons.tessellate + "</tessellate>")
                            .Append("<outerBoundaryIs>")
                            .Append("<LinearRing>")
                            .Append("<coordinates>" + placemark.ExportKmlPolygonPlacemarkPolygons.ExportKmlPolygonPlacemarkPolygonOuterBoundaryIs.ExportKmlPolygonPlacemarkPolygonOuterBoundaryIsLinearRings.coordinates + "</coordinates>")
                            .Append("</LinearRing>")
                            .Append("</outerBoundaryIs>")
                            .Append("</Polygon>")
                        .Append("</Placemark>");
                    }
                }

                xml.Append("</Document>")
                    .Append("</kml>");

            }
            return xml;
        }

    }

    public class ExportKmlPolygonDocument
    {
        public string name { get; set; }
        public IEnumerable<ExportKmlPolygonStyle> ExportKmlPolygonStyles => SetStyles();
        public IEnumerable<ExportKmlPolygonStyleMap> ExportKmlPolygonStyleMaps { get; set; }
        public IEnumerable<ExportKmlPolygonPlacemark> ExportKmlPolygonPlacemarks { get; set; }


        public IEnumerable<ExportKmlPolygonStyle> SetStyles()
        {
            ExportKmlPolygonStyle[] exportKmlPolygonStyles = new ExportKmlPolygonStyle[3];

            exportKmlPolygonStyles[0] = new ExportKmlPolygonStyle()
            {
                ExportKmlPolygonStylePolyStyles = new ExportKmlPolygonStylePolyStyle()
                {
                    color = "4cffffff"
                }
            };

            exportKmlPolygonStyles[1] = new ExportKmlPolygonStyle()
            {
                id = "sh_ylw-pushpin",
                ExportKmlPolygonStyleIconStyles = new ExportKmlPolygonStyleIconStyle()
                {
                    scale = "1.2"
                },
                ExportKmlPolygonStyleLineStyles = new ExportKmlPolygonStyleLineStyle()
                {
                    color = "ff000000"
                },
                ExportKmlPolygonStylePolyStyles = new ExportKmlPolygonStylePolyStyle()
                {
                    color = "4cffffff",
                    outline = "0"
                }
            };

            exportKmlPolygonStyles[2] = new ExportKmlPolygonStyle()
            {
                id = "sn_ylw-pushpin",
                ExportKmlPolygonStyleLineStyles = new ExportKmlPolygonStyleLineStyle()
                {
                    color = "ff000000"
                },
                ExportKmlPolygonStylePolyStyles = new ExportKmlPolygonStylePolyStyle()
                {
                    color = "4cffffff",
                    outline = "0"
                }
            };

            return exportKmlPolygonStyles;
        }

        public IEnumerable<ExportKmlPolygonStyleMap> SetStylesMaps()
        {
            ExportKmlPolygonStyleMap[] exportKmlPolygonStyleMaps = new ExportKmlPolygonStyleMap[3];
            ExportKmlPolygonStyleMapPair[] exportKmlPolygonStyleMapPairs = new ExportKmlPolygonStyleMapPair[2];

            exportKmlPolygonStyleMapPairs[0] = new ExportKmlPolygonStyleMapPair()
            {
                key = "normal",
                styleUrl = "#sn_ylw-pushpin"
            };

            exportKmlPolygonStyleMapPairs[1] = new ExportKmlPolygonStyleMapPair()
            {
                key = "highlight",
                styleUrl = "#sh_ylw-pushpin"
            };

            exportKmlPolygonStyleMaps[0] = new ExportKmlPolygonStyleMap()
            {
                id = "ExportKmlPolygonStyleMap",
                ExportKmlPolygonStyleMapPairs = exportKmlPolygonStyleMapPairs

            };

            return exportKmlPolygonStyleMaps;
        }

        public IEnumerable<ExportKmlPolygonPlacemark> SetPlacemarks(IEnumerable<Polygon> polygon)
        {
            var exportKmlPolygonPlacemarks = new ExportKmlPolygonPlacemark[] { };

            List<ExportKmlPolygonPlacemark> listPlacemark = new List<ExportKmlPolygonPlacemark>();

            StringBuilder coords = new StringBuilder();

            int c = 0;
            foreach (Polygon p in polygon)
            {
                foreach (string s in GetCoordinates(p))
                {
                    coords.AppendLine();
                    coords.AppendFormat("\t\t\t\t\t{0}", s);
                }

                listPlacemark.Add(new ExportKmlPolygonPlacemark()
                {
                    name = p.nome,
                    description = p.descricao,
                    styleUrl = "#msn_ylw-pushpin",
                    ExportKmlPolygonPlacemarkLookAts = new ExportKmlPolygonPlacemarkLookAt()
                    {
                        longitude = "-46.3186035156255",
                        latitude = "-18.652296381437083",
                        altitude = "11887",
                        tilt = "0",
                        gxAltitudeMode = "relativeToSeaFloor",
                        range = "1000"
                    },
                    ExportKmlPolygonPlacemarkPolygons = new ExportKmlPolygonPlacemarkPolygon()
                    {
                        ExportKmlPolygonPlacemarkPolygonOuterBoundaryIs = new ExportKmlPolygonPlacemarkPolygonOuterBoundaryIs()
                        {
                            ExportKmlPolygonPlacemarkPolygonOuterBoundaryIsLinearRings = new ExportKmlPolygonPlacemarkPolygonOuterBoundaryIsLinearRing()
                            {
                                coordinates = coords.ToString()
                            }
                        }
                    }
                });

                coords.Clear();

                //var teste = GetCoordinates(cerca);
                c++;
            }


            return listPlacemark;
        }

        public string[] GetCoordinates(Polygon polygon)
        {
            MatchCollection matches = Regex.Matches(polygon.coordenadas.ToString(), "([0-9-]+(\\.[0-9]+)? [0-9-]+(\\.[0-9]+)?)");
            string[] coordinates = new string[matches.Count];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = matches[i].ToString().Replace(" ", ",") + ",0 ";
            }

            return coordinates;
        }

    }

    #region Style
    public class ExportKmlPolygonStyle
    {
        public string id { get; set; }
        public ExportKmlPolygonStylePolyStyle ExportKmlPolygonStylePolyStyles { get; set; }
        public ExportKmlPolygonStyleLineStyle ExportKmlPolygonStyleLineStyles { get; set; }
        public ExportKmlPolygonStyleIconStyle ExportKmlPolygonStyleIconStyles { get; set; }
    }

    public class ExportKmlPolygonStylePolyStyle
    {
        public string color { get; set; }
        public string outline { get; set; }
    }
    public class ExportKmlPolygonStyleLineStyle
    {
        public string color { get; set; }
    }
    public class ExportKmlPolygonStyleIconStyle
    {
        public string scale { get; set; }
    }
    #endregion

    #region StyleMap
    public class ExportKmlPolygonStyleMap
    {
        public IEnumerable<ExportKmlPolygonStyleMapPair> ExportKmlPolygonStyleMapPairs { get; set; }
        public string id { get; set; }
        public string scale { get; set; }
        public string iconHref { get; set; }
    }

    public class ExportKmlPolygonStyleMapPair
    {
        public string key { get; set; }
        public string styleUrl { get; set; }
    }
    #endregion

    #region Placemark
    public class ExportKmlPolygonPlacemark
    {
        public string name { get; set; }
        public string styleUrl { get; set; }
        public string description { get; set; }
        public ExportKmlPolygonPlacemarkLookAt ExportKmlPolygonPlacemarkLookAts { get; set; }
        public ExportKmlPolygonPlacemarkPolygon ExportKmlPolygonPlacemarkPolygons { get; set; }
    }

    public class ExportKmlPolygonPlacemarkLookAt
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string altitude { get; set; }
        public string tilt { get; set; }
        public string gxAltitudeMode { get; set; }
        public string range { get; set; }
    }

    public class ExportKmlPolygonPlacemarkPolygon
    {
        public string tessellate { get; set; }
        public ExportKmlPolygonPlacemarkPolygonOuterBoundaryIs ExportKmlPolygonPlacemarkPolygonOuterBoundaryIs { get; set; }
    }

    public class ExportKmlPolygonPlacemarkPolygonOuterBoundaryIs
    {
        public ExportKmlPolygonPlacemarkPolygonOuterBoundaryIsLinearRing ExportKmlPolygonPlacemarkPolygonOuterBoundaryIsLinearRings { get; set; }
    }

    public class ExportKmlPolygonPlacemarkPolygonOuterBoundaryIsLinearRing
    {
        public string coordinates { get; set; }
    }
    #endregion

    public class ExportKmlPoint
    {
        public ExportKmlPointDocument ExportKmlPointDocuments { get; set; }
        public StringBuilder xml;


        public StringBuilder GerarXml()
        {
            xml = new StringBuilder();

            ExportKmlPointDocument document = null;
            if (ExportKmlPointDocuments != null)
                document = ExportKmlPointDocuments;

            if (document != null)
            {
                xml.Append("<?xml version='1.0' encoding='UTF-8'?>")
                    .Append("<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' ")
                    .Append("xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>")
                    .Append("<Document>")
                    .Append("<name>" + document.name + "</name>");

                if (document.Styles != null)
                {
                    foreach (var style in document.Styles)
                    {
                        xml.Append("<Style id=\"" + style.id + "\">")
                            .Append("<IconStyle>")
                            .Append("<scale>" + style.IconStyle.scale + "</scale>")
                            .Append("<Icon>")
                            .Append("<href>" + style.IconStyle.Icon.href + "</href>")
                            .Append("</Icon>")
                            .Append("</IconStyle>")
                            .Append("</Style>");
                    }
                }

                if (document.Placemarks != null)
                {
                    foreach (var placemark in Enumerable.ToList(document.Placemarks))
                    {
                        xml.Append("<Placemark>")
                            .Append("<name>" + placemark.name + "</name>")
                            .Append("<styleUrl>" + placemark.styleUrl + "</styleUrl>")
                            .Append("<description>" + placemark.description + "</description>")
                            .Append("<Point>")
                            .Append("<coordinates>" + placemark.Point.coordinates + "</coordinates>")
                            .Append("</Point>")
                            .Append("</Placemark>");
                    }
                }

                xml.Append("</Document>")
                    .Append("</kml>");
            }
            else {

                xml.Append("<?xml version='1.0' encoding='UTF-8'?>")
                 .Append("<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' ")
                 .Append("xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>")
                 .Append("<Document>")
                 .Append("<name></name>");

            }

            return xml;
        }

    }

    #region Document
    public class ExportKmlPointDocument
    {
        public string name { get; set; }

        public IEnumerable<ExportKmlPointStyle> Styles => SetStyles();

        public IEnumerable<ExportKmlPointPlacemark> Placemarks { get; set; }

        public IEnumerable<ExportKmlPointStyle> SetStyles()
        {
            ExportKmlPointStyle[] exportKmlPointStyleArray = new ExportKmlPointStyle[3];

            exportKmlPointStyleArray[0] = new ExportKmlPointStyle
            {
                id = "VERDE",
                IconStyle = new ExportKmlPointStyleIconStyle()
                {
                    scale = "0.7",
                    Icon = new ExportKmlPointStyleIconStyleIcon()
                    {
                        href = "http://dev.truckweb.com.br/Content/images/trucks/truck-green-E.png"
                    }
                }
            };

            exportKmlPointStyleArray[1] = new ExportKmlPointStyle
            {
                id = "VERMELHO",
                IconStyle = new ExportKmlPointStyleIconStyle()
                {
                    scale = "0.7",
                    Icon = new ExportKmlPointStyleIconStyleIcon()
                    {
                        href = "http://dev.truckweb.com.br/Content/images/trucks/truck-red-E.png"
                    }
                }
            };

            exportKmlPointStyleArray[2] = new ExportKmlPointStyle
            {
                id = "YELLOW",
                IconStyle = new ExportKmlPointStyleIconStyle()
                {
                    scale = "0.7",
                    Icon = new ExportKmlPointStyleIconStyleIcon()
                    {
                        href = "http://dev.truckweb.com.br/Content/images/trucks/truck-yellow-E.png"
                    }
                }
            };

            return exportKmlPointStyleArray;
        }
    }
    #endregion

    #region Style
    public class ExportKmlPointStyle
    {
        public string id { get; set; }
        public ExportKmlPointStyleIconStyle IconStyle { get; set; }
    }

    public class ExportKmlPointStyleIconStyle
    {
        public string scale { get; set; }
        public ExportKmlPointStyleIconStyleIcon Icon { get; set; }
    }

    public class ExportKmlPointStyleIconStyleIcon
    {
        public string href { get; set; }
    }
    #endregion

    #region Placemark
    public class ExportKmlPointPlacemark
    {
        public string name { get; set; }
        public string styleUrl { get; set; }
        public string description { get; set; }
        public ExportKmlPointPlacemarkPoint Point { get; set; }
    }

    public class ExportKmlPointPlacemarkPoint
    {
        public string coordinates { get; set; }
    }
    #endregion

}
