package com.bicis_tfg.bicimad_tfg_app.models;

import javax.annotation.Generated;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

@Generated("org.jsonschema2pojo")
public class RegisterModel {

    @SerializedName("Success")
    @Expose
    private boolean success;

    @SerializedName("Error")
    @Expose
    private String error;



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
     * The error
     */
    public String getError() {
        return error;
    }

    /**
     *
     * @param error
     * The Error
     */
    public void setError(String error) {
        this.error = error;
    }
}