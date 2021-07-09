package com.ubb.wp.Servlet.model;

import com.ubb.wp.Servlet.domain.Url;
import com.ubb.wp.Servlet.domain.User;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by forest.
 */
public class DBManager {
    private Connection connection;

    public DBManager() {
        connect();
    }

    public void connect() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/lab8", "root", "");
        } catch(Exception ex) {
            System.out.println("Error connecting:"+ex.getMessage());
            ex.printStackTrace();
        }
    }

    public User createAccount(String username, String password) {
        ResultSet rs;
        User u = null;
        try {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO users (username, password) VALUES  (?, ?)");
            stmt.setString(1, username);
            stmt.setString(2, password);
            rs = stmt.executeQuery();
            if (rs.next()) {
                u = new User(rs.getInt("id"), rs.getString("user"), rs.getString("password"));
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return u;
    }

    public User authenticate(String username, String password) {
        ResultSet rs;
        User u = null;
        System.out.println(username+" "+password);
        try {
            PreparedStatement stmt = connection.prepareStatement("SELECT * FROM users WHERE username = ? AND password = ?");
            stmt.setString(1, username);
            stmt.setString(2, password);
            rs = stmt.executeQuery();
            if (rs.next()) {
                u = new User(rs.getInt("id"), rs.getString("username"), rs.getString("password"));
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return u;
    }

    public boolean addUrl(Url url) {
        int rs;
        try {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO urls (url, user, clicks) VALUES (?, ?, ?)");
            stmt.setString(1, url.getUrl());
            stmt.setInt(2, url.getUserId());
            stmt.setInt(3, 0);
            rs = stmt.executeUpdate();

            return rs>0;

        } catch (SQLException e) {
            e.printStackTrace();
        }

        return false;
    }

    public ArrayList<Url> getUserUrls(int userid) {
        ArrayList<Url> assets = new ArrayList<Url>();
        ResultSet rs;
        try {
            PreparedStatement stmt = connection.prepareStatement("SELECT * FROM urls WHERE user = ?");
            stmt.setInt(1, userid);
            rs = stmt.executeQuery();
            while (rs.next()) {
                assets.add(new Url(
                        rs.getInt("id"),
                        rs.getInt("user"),
                        rs.getString("url"),
                        rs.getInt("clicks")
                ));
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return assets;
    }

    public boolean deleteUrl(int url, int user) {
        int r = 0;
        try {
            PreparedStatement stmt = connection.prepareStatement("DELETE FROM urls WHERE id = ? AND user = ?");
            stmt.setInt(1, url);
            stmt.setInt(2, user);
            r = stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        if (r>0) return true;
        else return false;
    }

    public List<Url> getTopUrls(int n) {
        ArrayList<Url> assets = new ArrayList<Url>();
        ResultSet rs;
        try {
            PreparedStatement stmt = connection.prepareStatement("SELECT * FROM urls ORDER BY clicks DESC LIMIT ?");
            stmt.setInt(1, n);
            rs = stmt.executeQuery();
            while (rs.next()) {
                assets.add(new Url(
                        rs.getInt("id"),
                        rs.getInt("user"),
                        rs.getString("url"),
                        rs.getInt("clicks")
                ));
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return assets;
    }

    public void incrementClick(int url) {
        try {
            PreparedStatement stmt = connection.prepareStatement("UPDATE urls SET clicks = clicks + 1 WHERE id = ?");
            stmt.setInt(1, url);
            stmt.executeQuery();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
