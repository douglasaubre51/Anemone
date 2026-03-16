package com.forge.anemone;

import com.forge.anemone.models.*;

import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.forge.anemone.models.MangaResponse;
import com.google.android.material.search.SearchBar;
import com.google.android.material.search.SearchView;
import com.google.gson.Gson;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class MainActivity extends AppCompatActivity {
    String url = "https://www.google.com";
    String mangadexUrl = "https://api.mangadex.org";

    RequestQueue _requestQueue;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        // Init Volley request queue!
        _requestQueue = Volley.newRequestQueue(this);

        SearchBar searchBox = findViewById(R.id.mangaSearchBox);
        SearchView searchView = findViewById(R.id.mangaSearchView);

        // show search view on search bar click!
        searchBox.setOnClickListener(v -> searchView.show());

        // handle search view submit!
        searchView.getEditText().setOnEditorActionListener((v,actionId,event)->{
            searchBox.setText(searchView.getText().toString());
            mangaSearchRequest(searchBox.getText().toString());
            searchView.hide();

            return false;
        });
    }

    public void mangaSearchRequest(String mangaTitle){
        SearchBar searchBox = findViewById(R.id.mangaSearchBox);

        StringRequest request = new StringRequest(
                Request.Method.GET,
                mangadexUrl + "/manga?title=" + mangaTitle,
                new Response.Listener<String>(){
                    @Override
                    public void onResponse(String json){
                        try {
                            System.out.println("Anemone got the message:");

                            Gson gson = new Gson();
                            MangaResponse mangaResponse = gson.fromJson(json, MangaResponse.class);

                            TextView mangaTitle = findViewById(R.id.mangaTitle);
                            TextView mangaDesc = findViewById(R.id.mangaDesc);

                                System.out.println(mangaResponse.data.get(0).attributes.title.get("ja-ro"));
                                mangaTitle.setText(mangaResponse.data.get(0).attributes.title.get("ja-ro"));

                                System.out.println(mangaResponse.data.get(0).attributes.description.get("en"));
                                mangaDesc.setText(mangaResponse.data.get(0).attributes.description.get("en"));
                        } catch (Exception e) {
                            System.out.println(e.getMessage());
                        }
                    }
                },
                new Response.ErrorListener(){
                    @Override
                    public void onErrorResponse(VolleyError error){
                        System.out.println("failed get request!");
                    }
                }
        );
        _requestQueue.add(request);
    }
}