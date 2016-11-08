package com.bicis_tfg.bicimad_tfg_app;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.view.inputmethod.EditorInfo;
import android.view.inputmethod.InputMethodManager;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.bicis_tfg.bicimad_tfg_app.helpers.ResourcesHelper;
import com.bicis_tfg.bicimad_tfg_app.models.LoginResult;
import com.bicis_tfg.bicimad_tfg_app.models.ValidResult;
import com.github.rahatarmanahmed.cpv.CircularProgressView;
import com.google.gson.Gson;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;
import services.IBiciMadServices;

/**
 * Created by JulioLopez on 5/7/16.
 */
public class LoginFragment extends Fragment {

    @BindView(R.id.email)
    AutoCompleteTextView mEmailView;
    @BindView(R.id.password)
    TextView mPasswordView;
    @BindView(R.id.email_sign_in_button)
    Button mEmailSignInButton;
    @BindView(R.id.email_register_button)
    Button mEmailRegisterButton;
    @BindView(R.id.email_login_form)
    LinearLayout linearLayout;
    @BindView(R.id.loading_image)
    ImageView loadingScreen;
    @BindView(R.id.loading_wheel)
    ImageView wheel;


    @Inject
    SharedPreferences sharedPref;
    @Inject
    IBiciMadServices apiService;
    @Inject
    ResourcesHelper rHelper;

    Animation fadeOutAnim;
    Animation rotateAnim;
    Animation.AnimationListener listener;

    @BindView(R.id.progress_view)
    CircularProgressView progressView;

    public LoginFragment() {
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        View view =  inflater.inflate(R.layout.fragment_login, container, false);
        // Set up the login form.
        BicimadApplication.inject(this);
        ButterKnife.bind(this, view);



        rotateAnim = AnimationUtils.loadAnimation(getContext(),
                R.anim.rotate);

        wheel.startAnimation(rotateAnim);

        fadeOutAnim = AnimationUtils.loadAnimation(getContext(),
                R.anim.fade_out);
        fadeOutAnim.setAnimationListener(new Animation.AnimationListener() {
            @Override
            public void onAnimationStart(Animation animation) {

            }

            @Override
            public void onAnimationEnd(Animation animation) {
                loadingScreen.setVisibility(View.GONE);
                wheel.setVisibility(View.GONE);
                wheel.setAlpha(0f);
            }

            @Override
            public void onAnimationRepeat(Animation animation) {

            }
        });

        listener = new Animation.AnimationListener() {
            @Override
            public void onAnimationStart(Animation animation) {

            }

            @Override
            public void onAnimationEnd(Animation animation) {
                Intent intent = new Intent(getView().getContext(), MainActivity.class);
                startActivity(intent);
                getActivity().finish();
            }

            @Override
            public void onAnimationRepeat(Animation animation) {

            }
        };

        isTokenValid();



        mPasswordView.setOnEditorActionListener((textView, id, keyEvent) -> {
            if (id == R.id.login || id == EditorInfo.IME_NULL) {
                attemptLogin();
                return true;
            }
            return false;
        });


        mEmailSignInButton.setOnClickListener(view1 -> attemptLogin());
        mEmailRegisterButton.setOnClickListener(view1 -> {
            RegisterFragment RegisterFragment = new RegisterFragment();
            FragmentTransaction trans = getFragmentManager().beginTransaction();
            trans.replace(R.id.fragmentcontainer, RegisterFragment);
            trans.addToBackStack(null);
            trans.commit();
        });
        return view;


    }

    private void isTokenValid() {
        String token = sharedPref.getString(getResources().getString(R.string.TokenKey), "");
        Observable<ValidResult> isValid = apiService.isTokenValid(token);
        isValid.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                .subscribe(valid -> {

                    if (valid.getValid()) {
                        fadeOutAnim.setAnimationListener(listener);
                        loadingScreen.startAnimation(fadeOutAnim);
                    }else{

                        linearLayout.setVisibility(View.VISIBLE);
                        loadingScreen.startAnimation(fadeOutAnim);
                        wheel.setVisibility(View.GONE);

                    }
                },throwable -> {
                    Snackbar.make(linearLayout, "There was a network error. Try again.", Snackbar.LENGTH_LONG).setAction("Action", null).show();
                    linearLayout.setVisibility(View.VISIBLE);
                    loadingScreen.startAnimation(fadeOutAnim);
                });
    }

    public void attemptLogin() {
        progressView.startAnimation();
        final InputMethodManager imm = (InputMethodManager) getActivity().getSystemService(Context.INPUT_METHOD_SERVICE);
        imm.hideSoftInputFromWindow(getView().getWindowToken(), 0);

        SharedPreferences.Editor editor = sharedPref.edit();
        Observable<LoginResult> result = apiService.logUser(mEmailView.getText().toString(), mPasswordView.getText().toString());

        result.subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(r -> {
                    progressView.stopAnimation();
                    if (r.isSuccess()) {
                        editor.putString(getResources().getString(R.string.TokenKey), r.getToken());
                        editor.commit();
                        Gson gson = new Gson();
                        String json = gson.toJson(r.getCurrentUser());
                        editor.putString(rHelper.getUserKey(), json);
                        editor.commit();
                        Intent intent = new Intent(getContext(), MainActivity.class);
                        startActivity(intent);
                        getActivity().finish();
                    } else {
                        Snackbar snackbar = Snackbar.make(linearLayout, "Login failed. Wrong username or password?", Snackbar.LENGTH_LONG)
                                .setAction("Action", null);
                        View snackbarView = snackbar.getView();
                        TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
                        textView.setTextColor(Color.RED);
                        snackbar.show();
                    }

                },throwable -> {
                    Snackbar.make(linearLayout, "There was a network error. Try again.", Snackbar.LENGTH_LONG).setAction("Try login", view -> {
                        isTokenValid();
                    }).show();
                });

    }




    }

