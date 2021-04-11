package com.tvd12.space_shooter.repo;

import com.tvd12.ezydata.mongodb.EzyMongoRepository;
import com.tvd12.ezyfox.database.annotation.EzyRepository;
import com.tvd12.space_shooter.entity.GameCurrentState;
import com.tvd12.space_shooter.entity.GameId;

@EzyRepository
public interface GameCurrentStateRepo
        extends EzyMongoRepository<GameId, GameCurrentState> {
}
