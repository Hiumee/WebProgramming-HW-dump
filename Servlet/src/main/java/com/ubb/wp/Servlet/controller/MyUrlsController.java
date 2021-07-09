package com.ubb.wp.Servlet.controller;

import com.ubb.wp.Servlet.domain.Url;
import com.ubb.wp.Servlet.domain.User;
import com.ubb.wp.Servlet.model.DBManager;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

@WebServlet(name = "urlServlet", value = "/myurls")
public class MyUrlsController extends HttpServlet {
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HttpSession session = request.getSession();
        User user = (User) session.getAttribute("user");
        DBManager dbManager = new DBManager();
        RequestDispatcher rd;

        if (user==null) {
            rd = request.getRequestDispatcher("/error.jsp");
            rd.forward(request, response);
        }
        else {
            List<Url> urls = dbManager.getUserUrls(user.getId());
            JSONArray jsonArray = new JSONArray();
            urls.forEach( url -> {
                        JSONObject jsonObject = new JSONObject();
                        jsonObject.put("id", url.getId());
                        jsonObject.put("user", url.getUserId());
                        jsonObject.put("url", url.getUrl());
                        jsonObject.put("clicks", url.getClicks());
                        jsonArray.add(jsonObject);
                    }
            );
            PrintWriter out = new PrintWriter(response.getOutputStream());
            out.println(jsonArray.toJSONString());
            out.flush();
        }
    }

    protected void doDelete(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HttpSession session = request.getSession();
        User user = (User) session.getAttribute("user");
        DBManager dbManager = new DBManager();
        RequestDispatcher rd;

        if (user!=null) {
            if(dbManager.deleteUrl(Integer.parseInt(request.getParameter("url")), user.getId())) {
                rd = request.getRequestDispatcher("/success.jsp");
                rd.forward(request, response);
            }
        }

        rd = request.getRequestDispatcher("/error.jsp");
        rd.forward(request, response);
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HttpSession session = request.getSession();
        User user = (User) session.getAttribute("user");
        DBManager dbManager = new DBManager();
        RequestDispatcher rd;

        if (user!=null) {
            Url url = new Url(0, user.getId(), request.getParameter("url"), 0);

            if(dbManager.addUrl(url)) {
                rd = request.getRequestDispatcher("/success.jsp");
                rd.forward(request, response);
            }
        }

        rd = request.getRequestDispatcher("/error.jsp");
        rd.forward(request, response);
    }
}
