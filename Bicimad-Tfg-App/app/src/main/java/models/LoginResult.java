package models;

import java.util.HashMap;
import java.util.Map;

public class LoginResult {

    private Boolean Success;
    private String Token;
    private Map<String, Object> additionalProperties = new HashMap<String, Object>();

    /**
     *
     * @return
     * The Success
     */
    public Boolean getSuccess() {
        return Success;
    }

    /**
     *
     * @param Success
     * The Success
     */
    public void setSuccess(Boolean Success) {
        this.Success = Success;
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