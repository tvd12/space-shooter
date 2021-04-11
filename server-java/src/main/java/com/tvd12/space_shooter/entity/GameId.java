package com.tvd12.space_shooter.entity;

import com.tvd12.ezydata.database.annotation.EzyCollectionId;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@EzyCollectionId
@NoArgsConstructor
@AllArgsConstructor
public class GameId {
    private String game;
    private long gameId;
}
