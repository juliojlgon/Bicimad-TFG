package com.bicis_tfg.bicimad_tfg_app;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;

import com.bicis_tfg.bicimad_tfg_app.helpers.ResourcesHelper;

import javax.inject.Inject;

import services.IBiciMadServices;

/**
 * A login screen that offers login via email/password.
 */
public class LoginActivity extends AppCompatActivity {



    @Inject
    SharedPreferences sharedPref;
    @Inject
    IBiciMadServices apiService;
    @Inject
    ResourcesHelper rHelper;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        // Set up the login form.
        BicimadApplication.inject(this);

        LoginFragment LoginFragment = new LoginFragment();
        FragmentTransaction trans = getSupportFragmentManager().beginTransaction();
        trans.replace(R.id.fragmentcontainer, LoginFragment);
        trans.commit();



    }




}

