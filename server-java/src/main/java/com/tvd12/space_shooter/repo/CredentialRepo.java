package com.tvd12.space_shooter.repo;

import com.tvd12.ezydata.mongodb.EzyMongoRepository;
import com.tvd12.ezyfox.database.annotation.EzyRepository;
import com.tvd12.space_shooter.entity.Credential;
import com.tvd12.space_shooter.entity.GamePlayerId;

@EzyRepository
public interface CredentialRepo
        extends EzyMongoRepository<GamePlayerId, Credential> {
}
