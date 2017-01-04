package com.bicis_tfg.bicimad_tfg_app.models;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by JulioLopez on 26/5/16.
 */
public class ValidResult {

    private Boolean Valid;
    private String Token;
    private Map<String, Object> additionalProperties = new HashMap<String, Object>();

    /**
     *
     * @return
     * The Valid
     */
    public Boolean getValid() {
        return Valid;
    }

    /**
     *
     * @param Valid
     * The Valid
     */
    public void setValid(Boolean Valid) {
        this.Valid = Valid;
    }

    /**
     *
     * @return
     * The Token
     */
    public String getToken() {
        return Token;
    }

    /**
     *
     * @param Token
     * The Token
     */
    public void setToken(String Token) {
        this.Token = Token;
    }

    public Map<String, Object> getAdditionalProperties() {
        return this.additionalProperties;
    }

    public void setAdditionalProperty(String name, Object value) {
        this.additionalProperties.put(name, value);
    }
}
