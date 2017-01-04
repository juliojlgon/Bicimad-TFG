
package com.bicis_tfg.bicimad_tfg_app.models;

import javax.annotation.Generated;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

@Generated("org.jsonschema2pojo")
public class User {

    @SerializedName("CurrentUser")
    @Expose
    private CurrentUser currentUser;

    /**
     * 
     * @return
     *     The currentUser
     */
    public CurrentUser getCurrentUser() {
        return currentUser;
    }

    /**
     * 
     * @param currentUser
     *     The CurrentUser
     */
    public void setCurrentUser(CurrentUser currentUser) {
        this.currentUser = currentUser;
    }

}
