package com.tvd12.space_shooter.request;

import com.tvd12.ezyfox.binding.annotation.EzyObjectBinding;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@EzyObjectBinding
public class GetGameIdRequest {
    private String gameName;
}
