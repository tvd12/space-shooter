package com.tvd12.space_shooter.repo;

import com.tvd12.ezydata.mongodb.EzyMongoRepository;
import com.tvd12.ezyfox.database.annotation.EzyRepository;
import com.tvd12.space_shooter.entity.LeaderBoard;

@EzyRepository
public interface LeaderBoardRepo
        extends EzyMongoRepository<LeaderBoard.Id, LeaderBoard> {
}
