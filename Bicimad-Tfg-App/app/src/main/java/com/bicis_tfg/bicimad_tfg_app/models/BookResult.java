package com.bicis_tfg.bicimad_tfg_app.models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import javax.annotation.Generated;

/**
 * Created by JulioLopez on 5/6/16.
 */
@Generated("org.jsonschema2pojo")
public class BookResult {


    @SerializedName("Success")
    @Expose
    private boolean success;
    @SerializedName("BikeId")
    @Expose
    private String bikeId;
    @SerializedName("Error")
    @Expose
    private String error;

    /**
     * @return The success
     */
    public boolean isSuccess() {
        return success;
    }

    /**
     * @param success The Success
     */
    public void setSuccess(boolean success) {
        this.success = success;
    }

    /**
     * @return The error
     */
    public String getError() {
        return error;
    }

    /**
     * @param error The Error
     */
    public void setError(String error) {
        this.error = error;
    }

    public String getBikeId() {
        return bikeId;
    }

    public void setBikeId(String bikeId) {
        this.bikeId = bikeId;
    }
}

