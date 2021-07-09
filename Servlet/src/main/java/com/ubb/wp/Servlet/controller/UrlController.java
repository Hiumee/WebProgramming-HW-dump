package com.ubb.wp.Servlet.controller;

import com.ubb.wp.Servlet.domain.Url;
import com.ubb.wp.Servlet.domain.User;
import com.ubb.wp.Servlet.model.DBManager;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

@WebServlet(name = "urlServlet", value = "/urls")
public class UrlController extends HttpServlet {
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        System.out.println("URLSLSAODAS");
        HttpSession session = request.getSession();
        User user = (User) session.getAttribute("user");
        DBManager dbManager = new DBManager();

        int number = 10;

        if (user != null && request.getParameter("count") != null) {
            number = Integer.parseInt(request.getParameter("count"));
        }

        List<Url> urls = dbManager.getTopUrls(number);
        JSONArray jsonArray = new JSONArray();
        urls.forEach(url -> {
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

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        DBManager dbManager = new DBManager();
        int url = Integer.parseInt(request.getParameter("url"));
        dbManager.incrementClick(url);
    }
}
