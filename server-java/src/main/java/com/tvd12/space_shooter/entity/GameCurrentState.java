package com.tvd12.space_shooter.entity;

import com.tvd12.ezyfox.annotation.EzyId;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class GameCurrentState {
    @EzyId
    private GameId id;
    private GameState state;
}
