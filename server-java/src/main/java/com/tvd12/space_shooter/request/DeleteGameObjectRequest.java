package com.tvd12.space_shooter.request;

import com.tvd12.ezyfox.binding.annotation.EzyObjectBinding;
import com.tvd12.space_shooter.model.Position;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@EzyObjectBinding
public class DeleteGameObjectRequest {
    private String gameName;
    private long gameId;
    private int objectId;
}
