package com.forge.anemone.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.forge.anemone.R;
import com.forge.anemone.models.MangaCoverModel;

import java.util.List;

public class MangaCollectionAdapter extends RecyclerView.Adapter<MangaCollectionAdapter.MangaCollectionViewHolder> {
    private List<MangaCoverModel> mangaCovers;
    public MangaCollectionAdapter(List<MangaCoverModel> mangaCoversList){
        mangaCovers = mangaCoversList;
    }

    @NonNull
    @Override
    public MangaCollectionViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.manga_card,parent,false);

        return new MangaCollectionViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull MangaCollectionViewHolder holder, int position) {
        MangaCoverModel mangaCover = mangaCovers.get(position);

        holder.titleText.setText( mangaCover.title);
        holder.descText.setText(mangaCover.desc);
    }

    @Override
    public int getItemCount() {
        return mangaCovers.size();
    }

    static class MangaCollectionViewHolder extends RecyclerView.ViewHolder{
        TextView titleText,descText;
        ImageView avatarImage;

        public MangaCollectionViewHolder(@NonNull View view){
            super(view);

            titleText = view.findViewById(R.id.cardTitleText);
            descText = view.findViewById(R.id.cardDescText);
            avatarImage = view.findViewById(R.id.avatarImageView);
        }
    }
}
