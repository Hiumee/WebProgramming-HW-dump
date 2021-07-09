package com.ubb.wp.Servlet.domain;

public class Url {
    private int id;
    private int userId;
    private String url;
    private int clicks;

    public Url(int id, int userId, String url, int clicks) {
        this.id = id;
        this.userId = userId;
        this.url = url;
        this.clicks = clicks;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public int getClicks() {
        return clicks;
    }

    public void setClicks(int clicks) {
        this.clicks = clicks;
    }
}
