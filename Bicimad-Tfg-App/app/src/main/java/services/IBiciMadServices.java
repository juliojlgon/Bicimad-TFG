package services;



import models.IndexResult;
import models.LoginResult;
import retrofit2.http.POST;
import retrofit2.http.Query;
import rx.Observable;

/**
 * Created by JulioLopez on 22/5/16.
 */
public interface IBiciMadServices {

    @POST("bike/returnindex")
    Observable<IndexResult> getSummaryData();

    @POST("Account/login")
    Observable<LoginResult> logUser(@Query("UserName") String username, @Query("Password") String password);
}
