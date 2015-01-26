//===============================================================================================================
// Author: Pavel Harbunou
//
// Requirements:
// * VS: Microsoft Visual Studio 2010 + SP1
// * .NET Framework: 4.0
// * OS: Windows 7+ (32-bit or 64-bit)
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;

namespace youtube_playlists
{
//===============================================================================================================
    public static class global
    {
        public const string string_app_name = "Application: Youtube-Playlists-to-HTML";
        public const string string_app_version = "Version: 1.0";
        public const string string_app_author = "Author: Pavel Harbunou";
        public const string string_web_user_agent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";
        public const string string_web_cookie_info = "f1=500000000&hl=en&gl=US";
        public const string string_web_youtube_users = "http://gdata.youtube.com/feeds/api/users/";
        public const string string_web_youtube_max_res = "/playlists?&max-results=50";
        public const string string_web_youtube_playlist_list = "playlist?list=";
        public const string string_web_youtube_domain = ".youtube.com";
    }
//===============================================================================================================
    static class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            string string_url;
                
            if (args.Length == 0)
            {
                Console.Write("\n");
                Console.Write(global.string_app_name);
                Console.Write("\n");
                Console.Write(global.string_app_author);
                Console.Write("\n");
                Console.Write(global.string_app_version);
                Console.Write("\n");
                Console.Write("Usage: youtube-playlists.exe [playlist-URL] [-body]\n");
                Console.Write("Parameter: [playlist-URL] - example: https://www.youtube.com/playlist?list=PLPMUUio-TPFwz7Z2SXfmCL7Bhigy8VjLm \n");
                Console.Write("Parameter: [-body] - save body only \n");
            }
            else
            {
                ypgenerator generator=new ypgenerator();
                bool save_body_only = args.Contains("-body");
                string_url=args[0];
                generator.get_playlists(string_url, save_body_only);
            }
            
       }            
            
    }
//===============================================================================================================
    internal class youtube_json
    {
        public string info { get; set; }
        public string content { get; set; }
    }
//===============================================================================================================
    public class youtube_playlist_data
    {
        public string url { get; set; }
        public string title { get; set; }
        public string data { get; set; }

        public youtube_playlist_data()
        {
        }

        public youtube_playlist_data(string urlvar)
        {
            this.url = urlvar;
        }
    };
//===============================================================================================================
    public class ypgenerator
    {

//---------------------------------------------------------------------------------------------------------------        
        public string replace_style_and_script(string HTML)
        {
            string string_x = "<(script|style)\\b[^>]*?>.*?</\\1>";
            return Regex.Replace(HTML, string_x, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
//---------------------------------------------------------------------------------------------------------------
        public void generate_html(string string_url, string string_filename, string youtube_playlist, bool bool_save_body_only)
        {
            string string_html_document = "";
            string string_document_name = string_filename;

            if (string_filename != "")
            {
                string_filename = WebUtility.HtmlDecode(string_filename);
                string_filename = new Regex("[\\/:*?\"'<>|]").Replace(string_filename, "");
                string string_file_path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + string_filename + ".html";
                System.IO.StreamWriter io_file = new System.IO.StreamWriter(string_file_path, false, Encoding.UTF8);

                if (!bool_save_body_only)
                {
                    string_html_document += "<!DOCTYPE html>" + "\r\n";
                    //string_html_document += "<html lang=\"en\">" + "\r\n";
                    string_html_document += "<!--[if IE 7]>" + "\r\n";
                    string_html_document += "<html class=\"ie ie7\" lang=\"ja\">"  + "\r\n";
                    string_html_document += "<![endif]-->"  + "\r\n";
                    string_html_document += "<!--[if IE 8]>"  + "\r\n";
                    string_html_document += "<html class=\"ie ie8\" lang=\"ja\">"  + "\r\n";
                    string_html_document += "<![endif]-->"  + "\r\n";
                    string_html_document += "<!--[if !(IE 7) | !(IE 8)  ]><!-->"  + "\r\n";
                    string_html_document += "<html lang=\"ja\">"  + "\r\n";
                    string_html_document += "<!--<![endif]-->"  + "\r\n";
                    string_html_document += "<head>"  + "\r\n";
                    string_html_document += "<meta charset=\"UTF-8\" />"  + "\r\n";
                    string_html_document += "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">"  + "\r\n";
                    string_html_document += "<!-- "  + "\r\n";
                    string_html_document += "<meta name=\"viewport\" content=\"width=800, user-scalable=yes, maximum-scale=1\">"  + "\r\n";
                    string_html_document += " -->"  + "\r\n";

                    string_html_document += "<title>" + string_filename + "</title>" + "\r\n";
                    string_html_document += "<link href=\"http://s.ytimg.com/yts/cssbin/www-core-vflqJi9JP.css\" rel=\"stylesheet\">" + "\r\n";
                    string_html_document += "<link href=\"http://s.ytimg.com/yts/cssbin/www-home-c4-vfljtKkXJ.css\" rel=\"stylesheet\">" + "\r\n";
                    string_html_document += "</head>" + "\r\n";
                    string_html_document += "<body>" + "\r\n";
                }

                Console.Write("\r\n");
                Console.Write("Youtube URL: ");
                Console.Write(string_url);
                Console.Write("\r\n");
                Console.Write("Playlist Name: ");
                Console.Write(string_document_name);
                Console.Write("\r\n");

                string_html_document += "<br><a href='" + string_url + "'><p align=\"center\">Playlist: " + string_document_name + "</p></a><br> \r\n";
                string_html_document += "<ul id=\"browse-items-primary\">" + "\r\n";
                string_html_document += "<li>" + "\r\n";
                string_html_document += "<div class=\"yt-uix-dragdrop pl-video-list-editable pl-video-list\" id=\"pl-video-list\">" + "\r\n";
                string_html_document += "<table class=\"pl-video-table\" id=\"pl-video-table\">" + "\r\n";
                string_html_document += "<tbody id=\"pl-load-more-destination\">" + "\r\n";
                string_html_document += youtube_playlist + "\r\n";
                string_html_document += "</tbody>" + "\r\n";
                string_html_document += "</table>" + "\r\n";
                string_html_document += "</li>" + "\r\n";
                string_html_document += "</ul>" + "\r\n";

                if (!bool_save_body_only)
                {
                    string_html_document += "</body>" + "\r\n";
                    string_html_document += "</html>" + "\r\n";
                }

                string_html_document = string_html_document.Replace("\"//", "\"http://");
                string_html_document = string_html_document.Replace("data-thumb=\"//", "data-thumb=\"http://");
                string_html_document = string_html_document.Replace("\"/watch?", "\"http://www.youtube.com/watch?");

                io_file.WriteLine(string_html_document);
                io_file.Close();

            }
        }
//---------------------------------------------------------------------------------------------------------------
        public string get_content(ref string string_search, string string_block_start, string string_block_end, bool bool_search = false)
        {
            int block_begin = 0;
            int block_end = 0;
            string string_result = "";

            try
            {
                block_begin = string_search.IndexOf(string_block_start);
                if (block_begin != -1)
                {
                    block_end = string_search.IndexOf(string_block_end, block_begin + string_block_start.Length);
                    if (block_end != -1)
                    {
                        if (bool_search)
                        {
                            string_result = string_search.Substring(block_begin, block_end - block_begin + string_block_end.Length);
                            string_search = string_search.Substring(block_end + string_block_end.Length, string_search.Length - (block_end + string_block_end.Length));
                        }
                        else
                        {
                            string_result = string_search.Substring(block_begin + string_block_start.Length, block_end - (string_block_start.Length + block_begin));
                            string_search = string_search.Substring(block_end + string_block_end.Length, string_search.Length - (block_end + string_block_end.Length));
                        }
                    }
                    else
                    {
                        string_search = "";
                    }
                }
                else
                {
                    string_search = "";
                }

            }
            catch
            {
                string_result = "";
                string_search = "";
            }

            return string_result;
        }
//---------------------------------------------------------------------------------------------------------------
        public byte[] get_image_from_url(string url)
        {
            Stream stream = null;
            byte[] byte_buffer;

            try
            {
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create(new UriBuilder(url).Uri.ToString());
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int) (response.ContentLength);
                    byte_buffer = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch
            {
                byte_buffer = null;
            }

            return (byte_buffer);
        }
//---------------------------------------------------------------------------------------------------------------
        public String image_url_to_base64(String url)
        {
            StringBuilder string_builder = new StringBuilder();
            if (url != "")
            {
                Byte[] byte_image = this.get_image_from_url(url);
                string_builder.Append(Convert.ToBase64String(byte_image, 0, byte_image.Length));
                return string_builder.ToString();
            }
            else
            {
                return "";
            }
        }
//---------------------------------------------------------------------------------------------------------------
        public List<youtube_playlist_data> get_playlists(string string_url, bool bool_generate_body_only)
        {

            bool bool_result;
            string string_playlist_url = string_url;
            string string_thumb_image = "";
            string string_thumb_uri_source = "";
            string string_thumb_uri_thumb = "";
            string string_result;
            string string_result_temp = "";
            string string_playlist_name = "";
            HttpWebRequest http_web_request;
            HttpWebResponse http_web_response;
            StreamReader stream_reader;
            StringBuilder string_list_items = new StringBuilder();
            List<youtube_playlist_data> list_yt_playlists = new List<youtube_playlist_data>();
            CookieContainer cookies_container = new CookieContainer();

            if (string_url.IndexOf("&page=") != -1)
            {
                string_url = string_url.Substring(0, string_url.IndexOf("&page="));
            }

            if (string_url.IndexOf(global.string_web_youtube_playlist_list) == -1)
            {
                http_web_request = (HttpWebRequest) WebRequest.Create(global.string_web_youtube_users + string_url + global.string_web_youtube_max_res);
                http_web_response = (HttpWebResponse) http_web_request.GetResponse();
                stream_reader = new StreamReader(http_web_response.GetResponseStream(), System.Text.Encoding.UTF8);
                string_result = stream_reader.ReadToEnd();
                stream_reader.Close();
                http_web_response.Close();
                do
                {
                    bool_result = string_result.Contains(global.string_web_youtube_playlist_list);
                    string string_result_content = "";
                    do
                    {
                        string_result_content = get_content(ref string_result, global.string_web_youtube_playlist_list, "\'", false);
                        if (string_result_content != "")
                        {
                            string_result_content = "https://www.youtube.com/playlist?list=" + string_result_content;
                            list_yt_playlists.Add(new youtube_playlist_data(new UriBuilder(string_result_content).Uri.ToString()));
                        }
                    } while (string_result_content != "");

                } while (bool_result);
            }
            else
            {
                string_url = new UriBuilder(string_url).Uri.ToString();
                list_yt_playlists.Add(new youtube_playlist_data(string_url));
            }

            foreach (youtube_playlist_data yt_playlist in list_yt_playlists)
            {
                string string_block = "";
                string string_list_item = "";
                string string_block_extras = "";
                string string_url_video = "";
                string string_title_video = "";
                string string_owner_video = "";
                string string_views = "";
                string string_results = "";
                string string_line = "";
                string string_url_search = "";
                int int_count = 0;
                int int_line_count = 0;

                string_url_search = yt_playlist.url;

                do
                {
                    int_count += 1;
                    http_web_request = (HttpWebRequest) WebRequest.Create(string_url_search);
                    http_web_request.UserAgent = global.string_web_user_agent;
                    Cookie cookie = new Cookie("PREF", global.string_web_cookie_info);
                    cookie.Domain = global.string_web_youtube_domain;
                    cookies_container.Add(cookie);
                    http_web_request.CookieContainer = cookies_container;
                    http_web_response = (HttpWebResponse) http_web_request.GetResponse();
                    stream_reader = new StreamReader(http_web_response.GetResponseStream(), System.Text.Encoding.UTF8);
                    string_result = stream_reader.ReadToEnd();
                    string_result_temp = string_result;
                    stream_reader.Close();
                    http_web_response.Close();

                    if (int_count == 1)
                    {
                        string_playlist_name = get_content(ref string_result_temp, "<h1 class=\"pl-header-title\">", "</h1>");
                        string_playlist_name = string_playlist_name.Replace("\n", "").Trim();
                        yt_playlist.title = string_playlist_name;
                    }
                    else
                    {
                        JavaScriptSerializer java_script_serializer = new JavaScriptSerializer();
                        youtube_json java_script_parsed = java_script_serializer.Deserialize <youtube_json>(string_result);
                        string_result = java_script_parsed.content;
                        string_result_temp = java_script_parsed.info;
                    }

                    bool_result = string_result.Contains("<tr class=\"pl-video yt-uix-tile");
                    do
                    {
                        string_list_item = get_content(ref string_result, "<tr class=\"pl-video yt-uix-tile", "</tr>", true);

                        string_list_item = string_list_item.Replace("src=\"//", "src=\"http://");
                        string_thumb_uri_source = string_list_item;
                        string_thumb_uri_source = get_content(ref string_thumb_uri_source, "src=\"", "\"");

                        if (string_thumb_uri_source != "")
                        {
                            if (string_thumb_uri_source.Substring(string_thumb_uri_source.Length - 3, 3) != "gif")
                            {
                                string_thumb_image = "data:image/png;base64," + image_url_to_base64(string_thumb_uri_source);
                                string_list_item = string_list_item.Replace(string_thumb_uri_source, string_thumb_image);
                            }
                            else
                            {
                                string_thumb_uri_thumb = string_list_item;
                                string_thumb_uri_thumb = get_content(ref string_thumb_uri_thumb, "data-thumb=\"//", "\"");

                                if (string_thumb_uri_thumb != "")
                                {
                                    string_thumb_image = "data:image/png;base64," + image_url_to_base64(string_thumb_uri_thumb);
                                    string_list_item = string_list_item.Replace(string_thumb_uri_source, string_thumb_image);
                                }
                            }
                        }

                        string_list_items.Append(string_list_item);
                        string_block = string_list_item; 
                        string_block_extras = string_block;

                        if (string_block_extras != "")
                        {
                            string_url_video = "https://www.youtube.com/watch?v=" + get_content(ref string_block_extras, "dir=\"ltr\" href=\"/watch?v=", "&amp");
                            string_title_video = WebUtility.HtmlDecode(get_content(ref string_block_extras, ">\n", "\n")).Trim();
                            string_owner_video = WebUtility.HtmlDecode(get_content(ref string_block_extras, "href=\"/user/", "\""));  
                            string_views = get_content(ref string_block_extras, "timestamp\">", "</");
                            string_line = string_title_video+ ": " + string_url_video;
                            string_results += string_line + "\r\n";
                            yt_playlist.data = string_results;
                            int_line_count += 1;
                        }
                     } while (string_block != "");

                    string_url_search = get_content(ref string_result_temp, "data-uix-load-more-href=\"", "\"");

                    if (!string.IsNullOrWhiteSpace(string_url_search))
                    {
                        string_url_search = "https://www.youtube.com" + string_url_search;
                        bool_result = true;
                    }
                    else
                    {
                        bool_result = false;
                    }

                } while (bool_result);

                generate_html(string_playlist_url, string_playlist_name, string_list_items.ToString(), bool_generate_body_only);
                 
            }

            return list_yt_playlists;
        }

    }
}
