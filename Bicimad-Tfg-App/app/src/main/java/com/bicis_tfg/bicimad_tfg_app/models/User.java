package com.bicis_tfg.bicimad_tfg_app.models;

import java.io.Serializable;

/**
 * Created by JulioLopez on 2/6/16.
 */
public class User {

    private String Username;
    private String Name;
    private String Avatar;
    private String email;

    public User(){}

    public String getUsername() {
        return Username;
    }

    public void setUsername(String username) {
        Username = username;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getAvatar() {
        return Avatar;
    }

    public void setAvatar(String avatar) {
        Avatar = avatar;
    }

    public String getEmail() {
        return email;
    }
}
