
package com.bicis_tfg.bicimad_tfg_app.models;

import javax.annotation.Generated;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

@Generated("org.jsonschema2pojo")
public class CurrentUser {

    @SerializedName("Id")
    @Expose
    private String id;
    @SerializedName("Email")
    @Expose
    private Object email;
    @SerializedName("Name")
    @Expose
    private String name;
    @SerializedName("Avatar")
    @Expose
    private Object avatar;
    @SerializedName("FriendlyUrlName")
    @Expose
    private Object friendlyUrlName;
    @SerializedName("IsAdmin")
    @Expose
    private boolean isAdmin;

    /**
     * 
     * @return
     *     The id
     */
    public String getId() {
        return id;
    }

    /**
     * 
     * @param id
     *     The Id
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     * 
     * @return
     *     The email
     */
    public Object getEmail() {
        return email;
    }

    /**
     * 
     * @param email
     *     The Email
     */
    public void setEmail(Object email) {
        this.email = email;
    }

    /**
     * 
     * @return
     *     The name
     */
    public String getName() {
        return name;
    }

    /**
     * 
     * @param name
     *     The Name
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * 
     * @return
     *     The avatar
     */
    public Object getAvatar() {
        return avatar;
    }

    /**
     * 
     * @param avatar
     *     The Avatar
     */
    public void setAvatar(Object avatar) {
        this.avatar = avatar;
    }

    /**
     * 
     * @return
     *     The friendlyUrlName
     */
    public Object getFriendlyUrlName() {
        return friendlyUrlName;
    }

    /**
     * 
     * @param friendlyUrlName
     *     The FriendlyUrlName
     */
    public void setFriendlyUrlName(Object friendlyUrlName) {
        this.friendlyUrlName = friendlyUrlName;
    }

    /**
     * 
     * @return
     *     The isAdmin
     */
    public boolean isIsAdmin() {
        return isAdmin;
    }

    /**
     * 
     * @param isAdmin
     *     The IsAdmin
     */
    public void setIsAdmin(boolean isAdmin) {
        this.isAdmin = isAdmin;
    }

}
