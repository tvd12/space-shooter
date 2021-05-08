package com.tvd12.space_shooter.repo;

import com.tvd12.ezydata.mongodb.EzyMongoRepository;
import com.tvd12.ezyfox.database.annotation.EzyQuery;
import com.tvd12.ezyfox.database.annotation.EzyRepository;
import com.tvd12.space_shooter.entity.GameObjectPosition;

import java.util.List;

@EzyRepository
public interface GameObjectPositionRepo
        extends EzyMongoRepository<GameObjectPosition.Id, GameObjectPosition> {
    @EzyQuery("{$and:[{'_id.game':?0},{'_id.gameId':?1}]}")
    List<GameObjectPosition> findByGameAndGameId(
            String game,
            long gameId
    );

    @EzyQuery("{$and:[{'_id.game':?0},{'_id.gameId':?1}]}")
    int deleteByGameAndGameId(
            String game,
            long gameId
    );
}
