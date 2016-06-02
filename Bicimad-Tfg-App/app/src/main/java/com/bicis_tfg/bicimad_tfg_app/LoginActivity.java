package com.bicis_tfg.bicimad_tfg_app;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.inputmethod.EditorInfo;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import models.LoginResult;
import models.ValidResult;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;
import services.IBiciMadServices;

/**
 * A login screen that offers login via email/password.
 */
public class LoginActivity extends AppCompatActivity {

    @BindView(R.id.email)
    AutoCompleteTextView mEmailView;
    @BindView(R.id.password)
    TextView mPasswordView;
    @BindView(R.id.email_sign_in_button)
    Button mEmailSignInButton;
    @BindView(R.id.email_login_form)
    LinearLayout linearLayout;

    @Inject
    SharedPreferences sharedPref;
    @Inject
    IBiciMadServices apiService;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        // Set up the login form.
        BicimadApplication.inject(this);
        ButterKnife.bind(this);

        String token = sharedPref.getString(getResources().getString(R.string.TokenKey), "");

        Observable<ValidResult> isValid = apiService.isTokenValid(token);
        isValid.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                .subscribe(valid -> {

                    if (valid.getValid()) {
                        Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                        startActivity(intent);
                        finish();
                    }
                });

        mPasswordView.setOnEditorActionListener((textView, id, keyEvent) -> {
            if (id == R.id.login || id == EditorInfo.IME_NULL) {
                attemptLogin();
                return true;
            }
            return false;
        });


        mEmailSignInButton.setOnClickListener(view -> attemptLogin());


    }

    public void attemptLogin() {
        SharedPreferences.Editor editor = sharedPref.edit();
        Observable<LoginResult> result = apiService.logUser(mEmailView.getText().toString(), mPasswordView.getText().toString());

        result.subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(r -> {
                    if (r.getSuccess()) {
                        editor.putString(getResources().getString(R.string.TokenKey), r.getToken());
                        editor.commit();
                        Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                        startActivity(intent);
                        finish();
                    } else {
                        Snackbar.make(linearLayout, "Something was wrong", Snackbar.LENGTH_LONG)
                                .setAction("Action", null).show();
                    }
                });

    }


}

