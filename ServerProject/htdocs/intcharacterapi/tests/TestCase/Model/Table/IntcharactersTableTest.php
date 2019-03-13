<?php
namespace App\Test\TestCase\Model\Table;

use App\Model\Table\IntcharactersTable;
use Cake\ORM\TableRegistry;
use Cake\TestSuite\TestCase;

/**
 * App\Model\Table\IntcharactersTable Test Case
 */
class IntcharactersTableTest extends TestCase
{

    /**
     * Test subject
     *
     * @var \App\Model\Table\IntcharactersTable
     */
    public $Intcharacters;

    /**
     * Fixtures
     *
     * @var array
     */
    public $fixtures = [
        'app.Intcharacters'
    ];

    /**
     * setUp method
     *
     * @return void
     */
    public function setUp()
    {
        parent::setUp();
        $config = TableRegistry::getTableLocator()->exists('Intcharacters') ? [] : ['className' => IntcharactersTable::class];
        $this->Intcharacters = TableRegistry::getTableLocator()->get('Intcharacters', $config);
    }

    /**
     * tearDown method
     *
     * @return void
     */
    public function tearDown()
    {
        unset($this->Intcharacters);

        parent::tearDown();
    }

    /**
     * Test initialize method
     *
     * @return void
     */
    public function testInitialize()
    {
        $this->markTestIncomplete('Not implemented yet.');
    }

    /**
     * Test validationDefault method
     *
     * @return void
     */
    public function testValidationDefault()
    {
        $this->markTestIncomplete('Not implemented yet.');
    }
}
