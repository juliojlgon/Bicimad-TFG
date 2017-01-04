package com.bicis_tfg.bicimad_tfg_app.models;

import javax.annotation.Generated;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

@Generated("org.jsonschema2pojo")
public class LoginResult {

    @SerializedName("Success")
    @Expose
    private boolean success;
    @SerializedName("Token")
    @Expose
    private String token;
    @SerializedName("CurrentUser")
    @Expose
    private CurrentUser currentUser;

    /**
     *
     * @return
     * The success
     */
    public boolean isSuccess() {
        return success;
    }

    /**
     *
     * @param success
     * The Success
     */
    public void setSuccess(boolean success) {
        this.success = success;
    }

    /**
     *
     * @return
     * The token
     */
    public String getToken() {
        return token;
    }

    /**
     *
     * @param token
     * The Token
     */
    public void setToken(String token) {
        this.token = token;
    }

    /**
     *
     * @return
     * The currentUser
     */
    public CurrentUser getCurrentUser() {
        return currentUser;
    }

    /**
     *
     * @param currentUser
     * The CurrentUser
     */
    public void setCurrentUser(CurrentUser currentUser) {
        this.currentUser = currentUser;
    }

}