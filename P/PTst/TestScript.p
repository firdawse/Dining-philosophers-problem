test tcDeadlockV1 [main=TestV1]:
  assert NoDeadlock in (union Dining, { TestV1 });

test tcNoDeadlockV2 [main=TestV2]:
  assert NoDeadlock in (union Dining, { TestV2 });
